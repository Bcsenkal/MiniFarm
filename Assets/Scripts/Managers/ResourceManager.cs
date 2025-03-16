using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<ProductType,int> productAmounts;

    protected override void Awake() 
    {
        base.Awake();
        productAmounts = new Dictionary<ProductType, int>();
        LoadResources();
    }

    private void LoadResources()
    {
        var productTypes = System.Enum.GetValues(typeof(ProductType));
        for(int i = 0; i < productTypes.Length; i++)
        {
            productAmounts.Add((ProductType)productTypes.GetValue(i),ES3.Load(productTypes.GetValue(i).ToString(),0));
            Debug.Log(productTypes.GetValue(i).ToString() + " : " + productAmounts[(ProductType)productTypes.GetValue(i)]);
        }
    }

    public int GetProductAmount(ProductType productType)
    {
        return productAmounts[productType];
    }

    public void AddProductAmount(ProductType productType, int amount)
    {
        productAmounts[productType] += amount;
        Debug.Log(productType.ToString() + " : " + productAmounts[productType]);
    }

    public void RemoveProductAmount(ProductType productType, int amount)
    {
        productAmounts[productType] -= amount;
    }
}
