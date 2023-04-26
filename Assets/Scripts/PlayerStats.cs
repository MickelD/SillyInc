using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats //REMEMBER THAT THE INITIALIZATION VALUES (THE ONES WITH THE "=") ARE THE DEFAULT VALUES WHEN THE STATS ARE RESET
                        //In Other words, they are the initial conditions
{
    #region "GAME STATS"
    public int day = 0;
    public int funds = 29210;
    public float reputation = 0.5f;
    public float stockValue = 1f;
    #endregion

    #region "HIDDEN STATS"
    public int marketSize = 5000;
    public float workerDisatisfaction = 0.05f;
    public int revenueForTheDay = 0;
    public int expensesForTheDay = 0;
    #endregion

    #region STRIKES 
    public bool onStrike;
    public Strike activeStrike;
    public float strikeChance = 0.05f;
    #endregion

    #region GLOBALVARIABLES 
    public float workerDesatisfactionWhenFired = 0.006f;
    public float workerSatistacionWhenHigherSalaries = 0.008f;
    public float workerDesatisfactionWhenLowerSalaries = 0.004f;
    public int baselineExpenses = 1000;
    public int minimumWage = 90;
    public int maximumWage = 450;
    #endregion

    #region CRYPTOCURRENCY 
    public int cryptoInStock = 0;
    public int cryptoValue = 10;
    #endregion

    #region DEPARTMENTS 
    public Department PatentDep = new Department(
        name: "SillyId",
        active: true,
        unlocked: true,
        //THIS DEFINES THE WORKERS REQUIRED TO CHANGE THE DAYS IT TAKES FOR THIS
        //DEPARTMENT'S DOCUMENT TO BE FILED. BY DEFINING A LIST OF PAIRS OF
        // NEW ([EMPLOYEE THRESHOLD], [DAYSFORNEWDOCUMENT])
        departmentTiers: new HiringTiers[] { new HiringTiers(employeeThreshold: 1, daysToFileNewDocument: 3), new(3, 2), new(6, 1) },
        employees: 2,
        salary: 180);

    public Department ProductionDep = new Department(
        name: "GG&W",
        active: true,
        unlocked: true,
        departmentTiers: new HiringTiers[] { new(30, 4), new(60, 3), new(100, 2), new(200, 1) },
        employees: 50,
        salary: 120);

    public Department AdsDep = new Department(
        name: "GoofDep",
        active: true,
        unlocked: true,
        departmentTiers: new HiringTiers[] { new(5, 5), new(10, 4), new(20, 3), new(45, 2) },
        employees: 12,
        salary: 200);

    public Department CryptoDep = new Department(
        name: "CryptoZoo",
        active: false,
        unlocked: false,
        departmentTiers: new HiringTiers[] { new(5, 7), new(10, 5), new(20, 3), new(50, 1) },
        employees: 0,
        salary: 300);

    #endregion

    #region PRODUCTS 

    public ProductSavedData basicDuck = new ProductSavedData(
        id: 0,
        amountInStock: 10000,
        productPopularity: 0.3f,
        hasBeenUnlocked: true,
        isInProduction: true,
        costToProduce: 3,
        price: 4,
        amountProducedPerWorker: 3
        );

    public ProductSavedData conducktor = new ProductSavedData(
        id: 1,
        amountInStock: 500,
        productPopularity: 0.3f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 5,
        price: 7,
        amountProducedPerWorker: 4
        );

    public ProductSavedData duckabel = new ProductSavedData(
        id: 2,
        amountInStock: 200,
        productPopularity: 0.5f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 10,
        price: 20,
        amountProducedPerWorker: 2
        );

    public ProductSavedData duckguard = new ProductSavedData(
        id: 3,
        amountInStock: 500,
        productPopularity: 0.4f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 5,
        price: 8,
        amountProducedPerWorker: 5
        );

    public ProductSavedData duckNorris = new ProductSavedData(
        id: 4,
        amountInStock: 200,
        productPopularity: 0.5f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 8,
        price: 15,
        amountProducedPerWorker: 2
        );

    public ProductSavedData ducktor = new ProductSavedData(
        id: 5,
        amountInStock: 500,
        productPopularity: 0.3f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 4,
        price: 15,
        amountProducedPerWorker: 6
        );

    public ProductSavedData dulk = new ProductSavedData(
        id: 6,
        amountInStock: 200,
        productPopularity: 0.6f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 8,
        price: 18,
        amountProducedPerWorker: 5
        );

    public ProductSavedData duckOfLiberty = new ProductSavedData(
        id: 7,
        amountInStock: 400,
        productPopularity: 0.5f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 17,
        price: 25,
        amountProducedPerWorker: 2
        );

    public ProductSavedData gentleduck = new ProductSavedData(
        id: 8,
        amountInStock: 100,
        productPopularity: 0.7f,
        hasBeenUnlocked: false,
        isInProduction: false,
        costToProduce: 20,
        price: 35,
        amountProducedPerWorker: 1
        );

    #endregion


}