using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayAdjustment : DocumentBase
{
    [Space(5), SerializeField] private EditableDepartmentGraph _patentDepGraph;
    [SerializeField] private EditableDepartmentGraph _productionDepGraph;
    [SerializeField] private EditableDepartmentGraph _adsDepGraph;
    [SerializeField] private EditableDepartmentGraph _cryptoDepGraph;

    public override void SetDocumentInfo()
    {
        _patentDepGraph.AffectedDep = gameInfo.PatentDep;
        _patentDepGraph.SetUpSliderAsWage(gameInfo.minimumWage, gameInfo.maximumWage);

        _productionDepGraph.AffectedDep = gameInfo.ProductionDep;
        _productionDepGraph.SetUpSliderAsWage(gameInfo.minimumWage, gameInfo.maximumWage);

        _adsDepGraph.AffectedDep = gameInfo.AdsDep;
        _adsDepGraph.SetUpSliderAsWage(gameInfo.minimumWage, gameInfo.maximumWage);

        _cryptoDepGraph.AffectedDep = gameInfo.CryptoDep;
        _cryptoDepGraph.SetUpSliderAsWage(gameInfo.minimumWage, gameInfo.maximumWage);

        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {
        CalculateDisatisfactionAndSetSalaries(_patentDepGraph);
        CalculateDisatisfactionAndSetSalaries(_productionDepGraph);
        CalculateDisatisfactionAndSetSalaries(_adsDepGraph);
        CalculateDisatisfactionAndSetSalaries(_cryptoDepGraph);

        base.SubmitDocument();
    }

    private void CalculateDisatisfactionAndSetSalaries(EditableDepartmentGraph graph)
    {
        int oldWage = graph.AffectedDep.salary;

        graph.AffectedDep.salary = graph.RealValue;

        if (oldWage > graph.RealValue) // WE HAVE LOWERED WAGES
        {
            gameInfo.workerDisatisfaction = Mathf.Clamp01(gameInfo.workerDisatisfaction + (oldWage - graph.RealValue) * gameInfo.workerDesatisfactionWhenLowerSalaries);
        }
        else //WE HAVE RAISED WAGES
        {
            gameInfo.workerDisatisfaction = Mathf.Clamp01(gameInfo.workerDisatisfaction - (graph.RealValue - oldWage) * gameInfo.workerSatistacionWhenHigherSalaries);
        }
    }
}
