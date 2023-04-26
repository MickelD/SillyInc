using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DocumentBase : MonoBehaviour
{
    [Header("Static Properties"), Space(3)]
    [SerializeField] protected StaticDocumentProperties _staticProperties;

    [Space(5), Header("Instance Properties"), Space(3)]
    [Space(3),SerializeField] private bool _canBeOpened;
    [SerializeField] private bool _canBeDiscarded;
    [SerializeField] public bool _canBeSubmitted;
    [SerializeField] private bool _isCompleted;
    [Space(5), SerializeField] private RectTransform _closedDocumentTransform;
    [SerializeField] private Vector3 _closedObjectTargetRotation;

    [HideInInspector] public bool CameFromQueue;
    [HideInInspector] public PlayerStats gameInfo;

    private Outline[] documentOutlines;

    private bool _pickepUp;
    private bool _hasBeenSubmitted;
    private bool _hasBeenDiscarded;
    private bool _raycastTarget;

    private Rigidbody2D _rb;
    private Animator _animatorController;
    private RectTransform _rectTransform;
    private Collider2D _hitbox;

    private PlayerController _playerController;
    private Canvas _canvas;

    private void Start()
    {
        _pickepUp = false;
        _hasBeenSubmitted = false;
        _hasBeenDiscarded = false;
        _raycastTarget = true;

        _playerController = transform.root.GetComponent<PlayerController>();
        _canvas = transform.root.GetComponent<Canvas>();

        _rectTransform = gameObject.GetComponent<RectTransform>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animatorController = gameObject.GetComponent<Animator>();
        _hitbox = gameObject.GetComponent<Collider2D>();

        documentOutlines = gameObject.GetComponentsInChildren<Outline>(includeInactive: true);
        SetAllOutlines(false);
    }

    public void DestroyDocument()
    {
        Destroy(gameObject);
    } 

    public virtual void DocumentDiscarded()
    {
        GameManager.OnSetShredder?.Invoke(false);
        DestroyDocument();
    }

    #region "DOCUMENT DATA"

    public virtual void SetDocumentInfo()
    {
        //CHILD DOCUMENTS SHOULD OVERRIDE THIS METHOD BUT CALL ITS DEFAULT IMPLEMENTATION VIA BASE.METHOD()

        StartCoroutine(RotateTowards(Random.Range(-_staticProperties.FileInRot, _staticProperties.FileInRot), _staticProperties.FileInTime));
        StartCoroutine(FileIn(Random.Range(_staticProperties.FileInDistance.x, _staticProperties.FileInDistance.y), _staticProperties.FileInTime));
    }

    #endregion

    #region "DOCUMENT ACTIONS"

    public void SetOpenDocument(bool open)
    {
        if (_canBeOpened)
        {
            _animatorController.SetBool(_staticProperties.OpenControlTag, open);
            SoundManager.Instance.sndPaperOpen.PlayAudioCue();
        }

        if (open == true)
        {
            StartCoroutine(RotateTowards(0f, _staticProperties.CorrectRotationTime));
        }
        else
        {
            StartCoroutine(RotateTowards(Random.Range(-_staticProperties.RandomClosingRotation, _staticProperties.RandomClosingRotation), _staticProperties.CorrectRotationTime));
        }
    }

    public virtual void SubmitDocument()
    {
        //CHILD DOCUMENTS SHOULD OVERRIDE THIS METHOD BUT CALL ITS DEFAULT IMPLEMENTATION VIA BASE.METHOD()

        DocumentFulfilled();

        SoundManager.Instance.sndSubmitDoc.PlayAudioCue();

        _raycastTarget = _hitbox.enabled = false;
        _hasBeenSubmitted = true;

        _rectTransform.SetPositionAndRotation(_playerController.SubmissionStartTransform.position, _playerController.SubmissionStartTransform.rotation);
        _closedDocumentTransform.eulerAngles = _closedObjectTargetRotation;

        _animatorController.SetTrigger(_staticProperties.SubmitTriggerTag);
    }

    public virtual void DiscardDocument()
    {
        //CHILD DOCUMENTS SHOULD OVERRIDE THIS METHOD BUT CALL ITS DEFAULT IMPLEMENTATION VIA BASE.METHOD()

        DocumentFulfilled();

        GameManager.OnSetShredder?.Invoke(true);

        _raycastTarget = _hitbox.enabled = false;
        _hasBeenDiscarded = true;

        _rectTransform.SetPositionAndRotation(_playerController.DiscardStartTransform.position, _playerController.DiscardStartTransform.rotation);
        _closedDocumentTransform.eulerAngles = _closedObjectTargetRotation;
        _rectTransform.SetParent(_playerController.DiscardStartTransform);

        _animatorController.SetTrigger(_staticProperties.DiscardTriggerTag);
    }

    public virtual void DocumentFulfilled()
    {
        if (CameFromQueue)
        {
            GameManager.Instance.MarkQueueDocumentAsFulfilled();
            GameManager.Instance.SendDocumentNextFromQueue();
        }
    }

    #endregion

    #region "DROP AND PICK UP"

    public void Drop()
    {
        if (_pickepUp)
        {
            //VFX & SFX
            SetAllOutlines(false);
            SoundManager.Instance.sndPaperDrop.PlayAudioCue();

            //unsubscribe to PlayerControllerEvents
            _playerController.OnOpenHeldDocument -= SetOpenDocument;
            _playerController.OnTrySubmitHeldDocument -= SubmitDocument;
            _playerController.OnDiscardHeldDocument -= DiscardDocument;
            _playerController.OnDropDocument -= Drop;

            //stop movement Corot.
            _pickepUp = false;
        }
    }

    public void PickUp(BaseEventData eventData)
    {
        if (!_pickepUp && _raycastTarget)
        {
            //move to bottom of the local transform list so as to make picked up document draw on top
            transform.SetAsLastSibling();

            //SFX & VFX
            SetAllOutlines(true);
            SoundManager.Instance.sndPaperPickUp.PlayAudioCue();

            //subscribe to PlayerControllerEvents
            _playerController.OnOpenHeldDocument += SetOpenDocument;
            _playerController.OnTrySubmitHeldDocument += SubmitDocument;
            _playerController.OnDiscardHeldDocument += DiscardDocument;
            _playerController.OnDropDocument += Drop;

            //start movement Corot.
            _pickepUp = true;
            StartCoroutine(UpdatePositionCoroutine((PointerEventData)eventData));
        }
    }

    #endregion

    #region "VISUAL EFFECTS"

    void SetAllOutlines(bool set)
    {
        foreach (Outline outline in documentOutlines)
        {
            outline.enabled = set;
        }
    }

    void SetOutlineColor(Color col)
    {
        foreach (Outline outline in documentOutlines)
        {
            outline.effectColor = col;
        }
    }

    #endregion

    #region "MOVEMENT AND ROTATION COROUTINES"

    private IEnumerator UpdatePositionCoroutine(PointerEventData pointerEventData)
    {
        Vector3 offset = _canvas.worldCamera.ScreenToWorldPoint((pointerEventData.position)) - _rectTransform.position;

        while (_pickepUp)
        {
            Vector3 pointerPosition = _canvas.worldCamera.ScreenToWorldPoint((pointerEventData.position));
            //transform.position = new Vector3(pointerPosition.x - offset.x, pointerPosition.y - offset.y, transform.position.z);
            _rb.MovePosition(new Vector3(pointerPosition.x - offset.x, pointerPosition.y - offset.y, _rectTransform.position.z));

            yield return null;
        }
    }
    private IEnumerator RotateTowards(float targetAngle, float dur)
    {
        float t = 0f;
        float startAngle = transform.eulerAngles.z;

        while (t < dur)
        {
            t += Time.deltaTime;

            float rot = Mathf.LerpAngle(startAngle, targetAngle, t / dur);

            transform.eulerAngles = new Vector3(0f, 0f, rot);

            yield return null;
        }
    }

    private IEnumerator FileIn(float dis, float t)
    {
        yield return null;

        _hitbox.enabled = false;
        _raycastTarget = false;

        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position - Vector3.up * dis;
        float elapsedTime = 0;

        while(elapsedTime < t)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _hitbox.enabled = true;
        _raycastTarget = true; 
    }

    #endregion

    #region "ACTION AREAS"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_staticProperties.OpenAreaTag))
        {
            SetOpenDocument(true);
        }
        else if (other.gameObject.CompareTag(_staticProperties.SubmitAreaTag) && _canBeSubmitted && _isCompleted)
        {
            SetOutlineColor(_staticProperties.PreSubmitHighlightTint);
        }
        else if (other.gameObject.CompareTag(_staticProperties.DiscardAreaTag) && _canBeDiscarded)
        {
            SetOutlineColor(_staticProperties.PreDiscardHighlightTint);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_staticProperties.OpenAreaTag))
        {
            SetOpenDocument(false);
        }

        SetOutlineColor(_staticProperties.SelectedHighLightTint);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_staticProperties.SubmitAreaTag) && _canBeSubmitted && !_pickepUp && _isCompleted && !_hasBeenSubmitted)
        {
            SubmitDocument();
        }
        else if (other.gameObject.CompareTag(_staticProperties.DiscardAreaTag) && _canBeDiscarded && !_pickepUp && !_hasBeenDiscarded)
        {
            DiscardDocument();
        }
    }

    #endregion


}