using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductSavedData
{
    public int id;
    public int amountInStock;
    public float productPopularity;
    public bool hasBeenUnlocked;
    public bool isInProduction;
    public int costToProduce;
    public int amountProducedPerWorker;
    public int price;

    public int producedAmount;
    public int soldAmount;

    public ProductSavedData(int id, int amountInStock, float productPopularity, bool hasBeenUnlocked, bool isInProduction, int costToProduce, int amountProducedPerWorker, int price)
    {
        this.id = id;
        this.amountInStock = amountInStock;
        this.productPopularity = productPopularity;
        this.hasBeenUnlocked = hasBeenUnlocked;
        this.isInProduction = isInProduction;
        this.costToProduce = costToProduce;
        this.amountProducedPerWorker = amountProducedPerWorker;
        this.price = price;
    }
}
