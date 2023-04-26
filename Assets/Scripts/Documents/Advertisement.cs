using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Advertisement : DocumentBase
{

    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _costText;

    [Header("Rewards Values"), Space(3)]
    [SerializeField] private float minRepReward;
    [SerializeField] private float maxRepReward;
    [SerializeField] private int minMarketReward;
    [SerializeField] private int maxMarketReward;
    [SerializeField] private int minCost;
    [SerializeField] private int maxCost;

    private float reputationReward;
    private int marketSizeReward;
    private int cost;
    private bool campaignApproved;
    [SerializeField] GameManager gm;

    public override void SetDocumentInfo()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetReward();
        SetCost();

        _rewardText.text = "+ " + marketSizeReward.ToString() + " clients";
        _costText.text = cost.ToString() + " $";
        
        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {

        if (campaignApproved)
        {
            gm.AddReputation(reputationReward);
            gm.AddFunds(-cost);
            gameInfo.expensesForTheDay += cost;
            gameInfo.marketSize += marketSizeReward;
        }
        
        base.SubmitDocument();
    }


    public void SetReward()
    {

        reputationReward = Random.Range(minRepReward, maxRepReward);
        marketSizeReward = Random.Range(minMarketReward, maxMarketReward);

    }

    public void SetCost()
    {

        cost = Random.Range(minCost, maxCost);
        if (cost > gameInfo.funds)
        {

            _canBeSubmitted = false;
            _costText.color = Color.red;
        }

    }

    public void ApproveCampaign(bool isApproved)
    {

        campaignApproved = isApproved;

    }

}
