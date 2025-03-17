using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
public enum BuildingType
{
    Spender,
    Generator
}
[Serializable]
public struct Requirement
{
    public Product product;
    public int amount;
}

public class Building : MonoBehaviour, IClickable
{
    [SerializeField] protected BuildingType buildingType;
    [SerializeField] protected BuildingData buildingData;
    protected ProductionHandler productionHandler;
    protected ProductionInfo productionInfo;
    public bool IsSelected{ get; set; }


    protected virtual void Awake()
    {
        productionHandler = GetComponent<ProductionHandler>();
        productionInfo = GetComponentInChildren<ProductionInfo>();
    }

    public virtual void Start()
    {
        Managers.EventManager.Instance.OnBuildingSelect += OnBuildingSelect;
    }

    public virtual void OnClick()
    {
        if(buildingType == BuildingType.Generator || IsSelected)
        {
            Harvest();
        }
        Managers.EventManager.Instance.ONOnBuildingSelect(this);
    }

    protected void OnBuildingSelect(Building building)
    {
        IsSelected = building == this;
    }

    protected void Harvest()
    {
        productionHandler.Harvest();
        if(buildingType == BuildingType.Generator) return;
        productionInfo.CheckProduction(productionHandler.HasQueue(),productionHandler.CanGetHarvested());
    }

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }

    public BuildingData GetBuildingData()
    {
        return buildingData;
    }

    public void UpdateTime(float timeLeft, float timeToProduce)
    {
        productionInfo.UpdateTime(timeLeft, timeToProduce);
    }

    public void UpdateAmount(int currentAmount,int queuedAmount)
    {
        productionInfo.UpdateAmount(currentAmount,queuedAmount);
    }

    public bool HasEnoughMaterials()
    {
        var result = true;
        foreach(var requirement in buildingData.inputProducts)
        {
            if(ResourceManager.Instance.GetProductAmount(requirement.product.type) < requirement.amount)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public void AddProduction()
    {
        productionHandler.AddProduction();
        productionInfo.CheckProduction(productionHandler.HasQueue(),productionHandler.CanGetHarvested());
    }

    public void RemoveProductionFromQueue()
    {
        productionHandler.RemoveProduction();
        productionInfo.CheckProduction(productionHandler.HasQueue(),productionHandler.CanGetHarvested());
    }

    public bool HasQueue()
    {
        if(buildingType == BuildingType.Generator) return true;
        return productionHandler.HasQueue();
    }

    public bool CanGetHarvested()
    {
        return productionHandler.CanGetHarvested();
    }

    public bool IsMaxCapacity()
    {
        return productionHandler.IsMaxCapacity();
    }

    public void IsFull()
    {
        productionInfo.IsFull();
    }
}
