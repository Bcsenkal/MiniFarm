using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProductRequirement
{
    public Product product;
    public int amount;
}

[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData", order = 0)]
public class BuildingData : ScriptableObject 
{
    public List<ProductRequirement> inputProducts;
    public Product outputProduct;
    public int outputAmount;
    public int capacity;
    public float productionTime;
    public string buildingName;
}
