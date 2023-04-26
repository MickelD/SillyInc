using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        GameManager.OnChangeMoneyUI += SetMoney;
    }

    private void OnDisable()
    {
        GameManager.OnChangeMoneyUI -= SetMoney;
    }

    void Start()
    {
        SetMoney(GameManager.Instance._currentSave.funds);
    }


    void SetMoney(int money )
    {
        _moneyText.text = money.ToString() + "$";
    }
}
