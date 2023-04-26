using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewProduct : DocumentBase
{

    [SerializeField] private Image _productImage;
    [SerializeField] private TextMeshProUGUI _productName;
    [SerializeField] private TextMeshProUGUI _productDescription;
    [SerializeField] private TextMeshProUGUI _productProductionCost;
    [SerializeField] private TextMeshProUGUI _productMarketPrice;

    [SerializeField] private List<Product> productsSO;

    private Product product;
    private ProductSavedData productSavedData;
    private int selectedProductID;
    private bool productApproved;

    private GameManager gm;



    public override void SetDocumentInfo()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        GetAllProductsLockedIDs();
        SelectProduct();

        _productImage.sprite = product.productImage;
        _productName.text = product.productName.ToString();
        _productDescription.text = product.description.ToString();
        _productProductionCost.text = "Cost of production: " + productSavedData.costToProduce.ToString() + " $/unit";
        _productMarketPrice.text = "Market Price: " + productSavedData.price.ToString() + " $/unit";

        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {

        if (productApproved)
        {
            productSavedData.hasBeenUnlocked = true;
        }
        
        base.SubmitDocument();
    }


    public void SelectProduct()
    {

        foreach (Product productSO in productsSO)
        {

            if (productSO.id == selectedProductID)
            {
                product = productSO;
            }

        }
        
    }

    
    public void GetAllProductsLockedIDs()
    {


        List<ProductSavedData> productsSavedDataInfo = gm.GetAllVarsFromTypeInSave<ProductSavedData>(gameInfo);

        List<int> productsIDs = new List<int>();

        foreach (ProductSavedData lockedProduct in productsSavedDataInfo)
        {
            if (!lockedProduct.hasBeenUnlocked)
            {

                productsIDs.Add(lockedProduct.id);
                
            }

        }

        selectedProductID = productsIDs[Random.Range(0, productsIDs.Count)];

        foreach (ProductSavedData productInfoGM in productsSavedDataInfo)
        {

            if (productInfoGM.id == selectedProductID)
            {

                productSavedData = productInfoGM;

            }

        }
    }

    public void ApproveProduct(bool isApproved)
    {

        productApproved = isApproved;

    }
}
