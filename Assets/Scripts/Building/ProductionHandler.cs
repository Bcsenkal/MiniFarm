using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionHandler : MonoBehaviour
{
    private Building building;
    private BuildingData productionData;
    private bool isGenerator;
    private int currentAmount = 0;
    private float nextProductionTime = 0;
    private float timer;
    private int queuedAmount = 0;

    void Awake()
    {
        building = GetComponent<Building>();
        productionData = building.GetBuildingData();
        isGenerator = productionData.inputProducts.Count <= 0;
    }

    private void Start()
    {
        building.UpdateAmount(currentAmount,queuedAmount);
        building.UpdateTime(0, productionData.productionTime);
        nextProductionTime = Time.time + productionData.productionTime;
    }

    private void Update()
    {
        Produce();
    }

    public void AddProduction()
    {
        if(queuedAmount < 1)
        {
            nextProductionTime = Time.time + productionData.productionTime;
            timer = 0;
        }
        queuedAmount++;
        ResourceManager.Instance.RemoveProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
        building.UpdateAmount(currentAmount,queuedAmount);
        Managers.EventManager.Instance.ONOnProductionQueueChange();
    }

    public void RemoveProduction()
    {
        Debug.Log("Removing production");
        queuedAmount--;
        ResourceManager.Instance.AddProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
        building.UpdateAmount(currentAmount,queuedAmount);
        if(queuedAmount <= 0) timer = 0;
        Managers.EventManager.Instance.ONOnProductionQueueChange();
    }

    private void Produce()
    {
        if(currentAmount == productionData.capacity) return;
        if(!isGenerator && queuedAmount == 0) return;
        timer += Time.deltaTime;
        building.UpdateTime(productionData.productionTime - timer, productionData.productionTime);
        if(Time.time < nextProductionTime) return;
        queuedAmount--;
        currentAmount += productionData.outputAmount;
        building.UpdateAmount(currentAmount,queuedAmount);
        timer = 0;
        Managers.EventManager.Instance.ONOnProductionComplete();
        if(currentAmount == productionData.capacity)
        {
            building.IsFull();
            return;
        } 
        nextProductionTime = Time.time + productionData.productionTime;
        if(isGenerator) return;
    }

    public void Harvest()
    {
        var harvestedAmount = currentAmount;
        if(harvestedAmount <= 0) return;
        currentAmount = 0;
        ResourceManager.Instance.AddProductAmount(productionData.outputProduct.type,harvestedAmount);
        building.UpdateAmount(currentAmount,queuedAmount);
        if(harvestedAmount < productionData.capacity) return;
        nextProductionTime = Time.time + productionData.productionTime;
    }

    public bool IsMaxCapacity()
    {
        return currentAmount + queuedAmount >= productionData.capacity;
    }

    public bool CanGetHarvested()
    {
        return currentAmount > 0;
    }

    public bool HasQueue()
    {
        return queuedAmount > 0;
    }
}
