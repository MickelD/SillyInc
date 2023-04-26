using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region "EVENTS"

    public static System.Action<bool> OnSetShredder;
    public static System.Action<int> OnChangeFunds;
    public static System.Action<int> OnUpdateDay;
    public static System.Action<float> OnChangeReputation;
    public static System.Action<int> OnChangeMoneyUI;
    public static System.Action<int> OnChangeActualQueUI;
    public static System.Action<int> OnChangeMaxQueUI;
    public static System.Action<int, int> OnUpdateFulFilledDocuments;
    #endregion

    public PlayerStats _currentSave;

    [Header("Components"), Space(3)]
    [SerializeField] private Transform _documentOriginPoint;
    [SerializeField] private Vector2 _documentSpawnHorRange;
    [SerializeField] private Transform _canvas;

    [Space(5), Header("Strike Rules"), Space(3)]
    [SerializeField] private Vector2Int _minMaxStrikeDuration;
    [SerializeField] private Vector2Int _minMaxStrikeAffectedDep;
    [SerializeField] private float strikeReputationPenalty;

    [Space(5), Header("Doc FileIn Queue"), Space(3)]
    [SerializeField] private Vector2 _minMaxTimeForNewDocument;
    private IEnumerator _currentlyActiveFileInTimer;
    private int _currentQueueIndex;
    private int _fulfilledDocuments;
    [SerializeField] private int openCryptoDay;

    public DocumentPrefabs DocumentPrefabs;

    private List<GameObject> _dailyyDocQueue;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        FileSaver.LoadFromJson();
    }

    private void Start()
    {
        _currentSave = FileSaver.savedFile;

        StartNewDay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            EndDay();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            FileSaver.ClearJson();
            EndDay();
        }

    }

    #region "DAY CYCLE"

    public void StartNewDay()
    {

        _currentSave.day++;

        CalculateExpenses();
        CalculateProductionAndRevenue();

        _currentSave.revenueForTheDay = 0;
        _currentSave.expensesForTheDay = 0;

        ManageStrikes();

        //Define document queue and Index
        _dailyyDocQueue = new List<GameObject>();
        _currentQueueIndex = 0;
        _fulfilledDocuments = 0;

        //Add compulsory docs to queue
        //THESE DOCS ARE ALWAYS FILED IN DAILY
        _dailyyDocQueue.Add(DocumentPrefabs.StaffAdjustment);
        _dailyyDocQueue.Add(DocumentPrefabs.PayAdjustment);

        //Determine which department-specific documents should we send in today
        DetermineDepartmentDocumentsToFileIn();
        OnUpdateFulFilledDocuments?.Invoke(_fulfilledDocuments, _dailyyDocQueue.Count);

        //Start to file in the queue. ONLY DO THIS AFTER THE QUEUE IS DEFINED AND WE WONT NEED TO ADD ANYTHING TO IT
        StartCoroutine(FileInTimer(3f));

        OnUpdateDay.Invoke(_currentSave.day);
    }

    public void EndDay()
    {
        FileSaver.SaveToJson();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator FileInTimer(float t)
    {
        yield return new WaitForSeconds(t);

        SendDocumentNextFromQueue();
    }

    public void SendDocumentNextFromQueue()
    {
        if (_currentlyActiveFileInTimer != null)
        {
            StopCoroutine(_currentlyActiveFileInTimer);
        }

        if (_currentQueueIndex < _dailyyDocQueue.Count)
        {
            CreateDocument(_dailyyDocQueue[_currentQueueIndex], true);

            _currentQueueIndex++;

            _currentlyActiveFileInTimer = FileInTimer(Random.Range(_minMaxTimeForNewDocument.x, _minMaxTimeForNewDocument.y));
            StartCoroutine(_currentlyActiveFileInTimer);
        }
    }

    public void MarkQueueDocumentAsFulfilled()
    {
        _fulfilledDocuments++;
        //OnChangeActualQueUI?.Invoke(_fulfilledDocuments);
        OnUpdateFulFilledDocuments?.Invoke(_fulfilledDocuments, _dailyyDocQueue.Count);
    }

    #endregion

    #region "DOCUMENT CREATION"
    public void DetermineDepartmentDocumentsToFileIn()
    {
        foreach (Department dep in GetAllActiveDepartments())
        {
            foreach (HiringTiers tier in dep.departmentTiers)
            {
                if (dep.employees <= tier.employeeThreshold)
                {
                    if (_currentSave.day % tier.daysToFileNewDocument == 0)
                    {
                        AddDepartmentDocumentToDailyQueue(dep);
                    }

                    break;
                }
            }
        }

        if (_currentSave.day >= openCryptoDay && !_currentSave.CryptoDep.unlocked)
        {
            _dailyyDocQueue.Add(DocumentPrefabs.OpenCrypto);
        }
    }

    public void AddDepartmentDocumentToDailyQueue(Department dep)
    {
        if (dep.Equals(_currentSave.PatentDep))
        {
            _dailyyDocQueue.Add(DocumentPrefabs.Patent);
        }
        else if (dep.Equals(_currentSave.ProductionDep))
        {
            _dailyyDocQueue.Add(DocumentPrefabs.ProductCatalog);
        }
        else if (dep.Equals(_currentSave.AdsDep))
        {
            _dailyyDocQueue.Add(DocumentPrefabs.AdPitch);
        }
        else if (dep.Equals(_currentSave.CryptoDep))
        {
            _dailyyDocQueue.Add(DocumentPrefabs.CryptoBuyOut);
        }
    }

    public void CreateDocument(GameObject docPrefab, bool fromQueue = false)
    {
        DocumentBase instantiatedDocument = Instantiate(docPrefab,
            _documentOriginPoint.position + Vector3.right * Random.Range(_documentSpawnHorRange.x, _documentSpawnHorRange.y),
            Quaternion.identity).GetComponent<DocumentBase>();

        instantiatedDocument.transform.SetParent(_canvas, true);

        instantiatedDocument.CameFromQueue = fromQueue;
        instantiatedDocument.gameInfo = _currentSave;

        instantiatedDocument.SetDocumentInfo();

        SoundManager.Instance.sndPaperEntry.PlayAudioCue();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(_documentOriginPoint.position + Vector3.right * _documentSpawnHorRange.x, _documentOriginPoint.position + Vector3.right * _documentSpawnHorRange.y);
    }

    #endregion

    #region "STRIKES"

    private void EndStrike()
    {
        foreach (Department dep in GetAllVarsFromTypeInSave<Department>(_currentSave))
        {
            dep.onStrike = false;
        }

        _currentSave.onStrike = false;
    }

    private void StartStrike(int duration, float reputationPenalty, Department[] deps)
    {
        _currentSave.onStrike = true;

        _currentSave.workerDisatisfaction = 0.05f;

        _currentSave.activeStrike = new Strike(
            strikeDuration: duration,
            reputationPenaltyPerDay: reputationPenalty,
            affectedDeps: deps
        );

        foreach (Department dep in _currentSave.activeStrike.affectedDeps)
        {
            dep.onStrike = true;
        }

        AddReputation(-strikeReputationPenalty);

        CreateDocument(DocumentPrefabs.StrikeFlyer);
    }

    private void ManageStrikes()
    {
        if (_currentSave.onStrike)
        {
            _currentSave.activeStrike.strikeDuration--;
            AddReputation(-strikeReputationPenalty);

            if (_currentSave.activeStrike.strikeDuration <= 0)
            {
                EndStrike();
            }
            else
            {
                CreateDocument(DocumentPrefabs.StrikeFlyer);
            }

        }
        else if (Random.value <= _currentSave.strikeChance + _currentSave.workerDisatisfaction)
        {
            List<Department> affectedDeps = GetAllActiveDepartments().ToList();

            int numberOfAffectedDeps = affectedDeps.Count - Random.Range(_minMaxStrikeAffectedDep.x, _minMaxStrikeAffectedDep.y + 1);

            for (int i = 0; i < numberOfAffectedDeps; i++)
            {
                affectedDeps.RemoveAt(i);
            }

            StartStrike(Random.Range(_minMaxStrikeDuration.x, _minMaxStrikeDuration.y + 1), strikeReputationPenalty, affectedDeps.ToArray());

        }
    }

    #endregion

    #region "UTILITY"

    public Department[] GetAllActiveDepartments()
    {
        return (from d in GetAllVarsFromTypeInSave<Department>(_currentSave) where d.active && !d.onStrike  select d).ToArray();
    }


    public Department[] GetAllDepartmentsOnStrike()
    {
        return (from s in GetAllVarsFromTypeInSave<Department>(_currentSave) where s.onStrike select s).ToArray();
    }


    public void CalculateProductionAndRevenue()
    {
        foreach (ProductSavedData product in GetAllVarsFromTypeInSave<ProductSavedData>(_currentSave))
        {
            //CALCULATE PRODUCTION AND ADD IT TO STOCK
            if (product.isInProduction)
            {
                product.producedAmount = _currentSave.ProductionDep.employees * product.amountProducedPerWorker * _currentSave.ProductionDep.active.GetHashCode();
                _currentSave.expensesForTheDay += product.producedAmount * product.costToProduce;
                product.amountInStock += product.producedAmount;
            }


            //SALES (clamping to the ammount we have)
            if (product.hasBeenUnlocked)
            {
                int sales = Mathf.RoundToInt(Mathf.Clamp(_currentSave.marketSize * _currentSave.reputation * product.productPopularity, 0, product.amountInStock));

                product.soldAmount = sales;
                product.amountInStock -= sales;

                _currentSave.revenueForTheDay += sales * product.price;
            }
            
        }

        AddFunds(_currentSave.revenueForTheDay);

        AddFunds(-(_currentSave.expensesForTheDay));

        CreateDocument(DocumentPrefabs.ProfitsMemo);
    }

    public void CalculateExpenses()
    {
        foreach (Department dep in GetAllActiveDepartments())
        {
            _currentSave.expensesForTheDay += dep.employees * dep.salary;
        }

        foreach (Department strikeDep in GetAllDepartmentsOnStrike())
        {
            _currentSave.expensesForTheDay += strikeDep.employees * strikeDep.salary;
        }

    }

    public List<T> GetAllVarsFromTypeInSave<T>(PlayerStats save)
    {
        List<T> foundVars = new List<T>();

        foreach (System.Reflection.FieldInfo field in save.GetType().GetFields())
        {
            if (field.FieldType.Equals(typeof(T)))
            {
                foundVars.Add((T)field.GetValue(save));
            }
        }

        return foundVars;
    }

    #endregion

    #region "VARIABLE MANIPULATION"

    public void AddFunds(int funds)
    {
        _currentSave.funds = Mathf.Clamp(_currentSave.funds + funds, 0, _currentSave.funds + funds);
        OnChangeFunds?.Invoke(_currentSave.funds);
        OnChangeMoneyUI?.Invoke(_currentSave.funds);
    }

    public void AddReputation(float rep)
    {
        _currentSave.reputation = Mathf.Clamp01(_currentSave.reputation + rep);
        OnChangeReputation?.Invoke(_currentSave.reputation);
    }

    #endregion
}

[System.Serializable]
public class DocumentPrefabs
{
    public GameObject TutorialFlyer;    
    public GameObject ProfitsMemo;
    public GameObject StaffAdjustment;
    public GameObject PayAdjustment;
    public GameObject AdPitch;
    public GameObject Patent;
    public GameObject ProductCatalog;
    public GameObject StrikeFlyer;
    public GameObject CryptoBuyOut;
    public GameObject OpenCrypto;
}
