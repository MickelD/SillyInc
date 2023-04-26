using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Benefits : DocumentBase
{

    [Header("Interface References - Revenues and Expenses"), Space(3)]
    [SerializeField] private TextMeshProUGUI _revenueText;
    [SerializeField] private TextMeshProUGUI _expensesText;

    [Header("Product References"), Space(3)]
    [SerializeField] private BenefitsOperations _basicDuck;
    [SerializeField] private BenefitsOperations _conducktor;
    [SerializeField] private BenefitsOperations _duckabel;
    [SerializeField] private BenefitsOperations _duckguard;
    [SerializeField] private BenefitsOperations _duckNorris;
    [SerializeField] private BenefitsOperations _ducktor;
    [SerializeField] private BenefitsOperations _dulk;
    [SerializeField] private BenefitsOperations _duckOfLiberty;
    [SerializeField] private BenefitsOperations _gentleduck;

    public override void SetDocumentInfo()
    {

        _revenueText.text = gameInfo.revenueForTheDay.ToString();
        _expensesText.text = gameInfo.expensesForTheDay.ToString();

        _basicDuck.affectedProduct = gameInfo.basicDuck;
        _basicDuck.SetProfitsInformation();

        _conducktor.affectedProduct = gameInfo.conducktor;
        _conducktor.SetProfitsInformation();

        _duckabel.affectedProduct = gameInfo.duckabel;
        _duckabel.SetProfitsInformation();

        _duckguard.affectedProduct = gameInfo.duckguard;
        _duckguard.SetProfitsInformation();

        _duckNorris.affectedProduct = gameInfo.duckNorris;
        _duckNorris.SetProfitsInformation();

        _ducktor.affectedProduct = gameInfo.ducktor;
        _ducktor.SetProfitsInformation();

        _dulk.affectedProduct = gameInfo.dulk;
        _dulk.SetProfitsInformation();

        _duckOfLiberty.affectedProduct = gameInfo.duckOfLiberty;
        _duckOfLiberty.SetProfitsInformation();

        _gentleduck.affectedProduct = gameInfo.gentleduck;
        _gentleduck.SetProfitsInformation();

        base.SetDocumentInfo();
    }
}
