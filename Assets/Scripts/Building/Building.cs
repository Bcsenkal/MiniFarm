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

[Serializable]
public struct ProductionData
{
    public Product product;
    public int productionAmount;
    public int productionTime;
    public int capacity;
    public bool isRaw;
    [HideIf("isRaw")]public List<Requirement> prerequisites;
}

public class Building : MonoBehaviour, IClickable
{
    [SerializeField] protected BuildingType buildingType;
    [SerializeField] protected ProductionData productionData;
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
        Managers.EventManager.Instance.OnClick += CloseUIElements;
    }

    public virtual void CloseUIElements(IClickable clickable)
    {
        if(clickable as Building == this) return;
        IsSelected = false;
    }

    public virtual void OnClick()
    {
        IsSelected = true;
    }

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }

    public ProductionData GetProductionData()
    {
        return productionData;
    }

    public void UpdateTime(float timeLeft, float timeToProduce)
    {
        productionInfo.UpdateTime(timeLeft, timeToProduce);
    }

    public void UpdateAmount(int currentAmount)
    {
        productionInfo.UpdateAmount(currentAmount);
    }

    public void IsFull()
    {
        productionInfo.IsFull();
    }
}
