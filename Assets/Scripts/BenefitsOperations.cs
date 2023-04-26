using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BenefitsOperations : MonoBehaviour
{

    public PlayerStats gameStats;

    [Header("Interface References - Products"), Space(3)]
    [SerializeField] private TextMeshProUGUI _producedAmount;
    [SerializeField] private TextMeshProUGUI _soldAmount;
    [SerializeField] private TextMeshProUGUI _stock;

    [HideInInspector] public ProductSavedData affectedProduct;

    private int production;
    private int sales;

    public void SetProfitsInformation()
    {

        if (affectedProduct.hasBeenUnlocked)
        {

            _producedAmount.text = affectedProduct.producedAmount.ToString() + " u";
            _soldAmount.text = affectedProduct.soldAmount.ToString() + " u";
            _stock.text = affectedProduct.amountInStock.ToString() + " u";
        }
        else
        {

            _producedAmount.text = 0.ToString() + " u";
            _soldAmount.text = sales.ToString() + " u";
            _stock.text = 0.ToString() + " u";
        }
        

    }

}
