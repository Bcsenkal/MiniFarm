using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionHandler : MonoBehaviour
{
    private Building building;
    private ProductionData productionData;
    private bool isGenerator;
    private int currentAmount = 0;
    private float nextProductionTime = 0;
    private float timer;
    private int queuedAmount = 0;

    void Awake()
    {
        building = GetComponent<Building>();
        productionData = building.GetProductionData();
        isGenerator = building.GetBuildingType() == BuildingType.Generator;
    }

    private void Start()
    {
        building.UpdateAmount(currentAmount);
        building.UpdateTime(0, productionData.productionTime);
        nextProductionTime = Time.time + productionData.productionTime;
    }

    private void Update()
    {
        Produce();
    }

    private void Produce()
    {
        if(currentAmount == productionData.capacity) return;
        if(!isGenerator && queuedAmount == 0) return;
        timer += Time.deltaTime;
        building.UpdateTime(productionData.productionTime - timer, productionData.productionTime);
        if(Time.time < nextProductionTime) return;
        currentAmount += productionData.productionAmount;
        building.UpdateAmount(currentAmount);
        timer = 0;
        if(currentAmount == productionData.capacity)
        {
            building.IsFull();
            return;
        } 
        nextProductionTime = Time.time + productionData.productionTime;
    }

    public void Harvest()
    {
        var harvestedAmount = currentAmount;
        if(harvestedAmount <= 0) return;
        currentAmount = 0;
        ResourceManager.Instance.AddProductAmount(productionData.product.type,harvestedAmount);
        building.UpdateAmount(currentAmount);
        if(harvestedAmount < productionData.capacity) return;
        nextProductionTime = Time.time + productionData.productionTime;
    }
}
