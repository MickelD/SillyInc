using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaffAdjustment : DocumentBase
{
    [Space(5), SerializeField] private EditableDepartmentGraph _patentDepGraph;
    [SerializeField] private EditableDepartmentGraph _productionDepGraph;
    [SerializeField] private EditableDepartmentGraph _adsDepGraph;
    [SerializeField] private EditableDepartmentGraph _cryptoDepGraph;

    public override void SetDocumentInfo()
    {
        _patentDepGraph.AffectedDep = gameInfo.PatentDep;
        _patentDepGraph.SetUpSliderAsEmployeeCount();

        _productionDepGraph.AffectedDep = gameInfo.ProductionDep;
        _productionDepGraph.SetUpSliderAsEmployeeCount();

        _adsDepGraph.AffectedDep = gameInfo.AdsDep;
        _adsDepGraph.SetUpSliderAsEmployeeCount();

        _cryptoDepGraph.AffectedDep = gameInfo.CryptoDep;
        _cryptoDepGraph.SetUpSliderAsEmployeeCount();

        base.SetDocumentInfo();
    }

    //THERE IS NOTHING TO DO WHEN THE DOCUMENT IS DISCARDED BECAUSE WE WILL JUST NOT USE THE VALUES THE PLAYER HAS SET.
    //THIS MEANS WE CAN LEAVE THE DEFAULT VIRTUAL FUNCTIONALITY, WHICH JUST DESTROYS THE OBJECT

    //public override void DiscardDocument()
    //{
    //    base.DiscardDocument();
    //}

    //WE MUST APPLY THE CHANGES MADE IN THE DOCUMENT TO THE PLAYER SAVE FILE
    public override void SubmitDocument()
    {
        CalculateDisatisfactionAndSetEmployees(_patentDepGraph);
        CalculateDisatisfactionAndSetEmployees(_productionDepGraph);
        CalculateDisatisfactionAndSetEmployees(_adsDepGraph);
        CalculateDisatisfactionAndSetEmployees(_cryptoDepGraph);

        base.SubmitDocument();
    }

    private void CalculateDisatisfactionAndSetEmployees(EditableDepartmentGraph graph)
    {
        int oldStaff = graph.AffectedDep.employees;

        graph.AffectedDep.SetEmployees(graph.RealValue);

        if (oldStaff > graph.RealValue) // WE HAVE FIRED PEOPLE
        {
            gameInfo.workerDisatisfaction = Mathf.Clamp01(gameInfo.workerDisatisfaction + (oldStaff - graph.RealValue) * gameInfo.workerDesatisfactionWhenFired);
        }
    }

}
