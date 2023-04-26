using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CryptoMarketDoc : DocumentBase
{

    [Header("Interface References"), Space(3)]
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _ownedCryptoText;
    [SerializeField] private Slider _sliderComponent;
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _currentOwnedText;

    [Header("Values"), Space(3)]
    [SerializeField] private int minCryptoValue;
    [SerializeField] private int maxCryptoValue;

    private int dayValue;
    private int operationMoney;
    private int operationCryptoAmount;
    private bool hasSold = false;
    [SerializeField] GameManager gm;

    public override void SetDocumentInfo()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetValue();

        _valueText.text = " " + dayValue + " $/DC";
        _ownedCryptoText.text = " " + gameInfo.cryptoInStock + " DuckCoins";

        SetUpSlider();

        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {

        CalculateExpensesOrBenefits();

        if (!hasSold)
        {
            gm.AddFunds(-operationMoney);
            gameInfo.expensesForTheDay += operationMoney;
            gameInfo.cryptoInStock += operationCryptoAmount;
        }
        else if (hasSold)
        {
            gm.AddFunds(operationMoney);
            gameInfo.revenueForTheDay += operationMoney;
            gameInfo.cryptoInStock -= operationCryptoAmount;
        }

        base.SubmitDocument();
    }

    public void SetValue()
    {

        dayValue = Random.Range(minCryptoValue, maxCryptoValue);
        

    }

    public void SetUpSlider()
    {

        _sliderComponent.maxValue = (gameInfo.funds / dayValue) + gameInfo.cryptoInStock; //Should be revised to set the correct maximum
        _sliderComponent.minValue = 0;
        _sliderComponent.value = gameInfo.cryptoInStock;
        _currentOwnedText.text = gameInfo.cryptoInStock.ToString();
        _sliderComponent.onValueChanged.AddListener((value) => { _currentOwnedText.text = value.ToString(); });
        _fillImage.fillAmount = gameInfo.cryptoInStock;  

    }

    public void CalculateExpensesOrBenefits()
    {

        if (_sliderComponent.value < gameInfo.cryptoInStock)
        {

            operationCryptoAmount = (int)(gameInfo.cryptoInStock - _sliderComponent.value);
            hasSold = true;
        }
        else if (_sliderComponent.value > gameInfo.cryptoInStock)
        {

            operationCryptoAmount = (int)(_sliderComponent.value - gameInfo.cryptoInStock);
            hasSold = false;
        }

        operationMoney = operationCryptoAmount * dayValue;


    }
}
