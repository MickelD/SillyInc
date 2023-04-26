using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrikeInfo : DocumentBase
{
    [Space(5), SerializeField] private TextMeshProUGUI _departmentsText;
    [SerializeField] private TextMeshProUGUI _daysText;

    public override void SetDocumentInfo()
    {
        if (gameInfo.onStrike)
        {
            _departmentsText.text = "";

            foreach (Department dep in gameInfo.activeStrike.affectedDeps)
            {
                _departmentsText.text += dep.name + "\n"; 
            }

            if (gameInfo.activeStrike.strikeDuration > 1)
            {
                _daysText.text = gameInfo.activeStrike.strikeDuration.ToString() + " DAYS";
            }
            else
            {
                _daysText.text = "1 DAY";
            }

            base.SetDocumentInfo();
        }
    }


}
