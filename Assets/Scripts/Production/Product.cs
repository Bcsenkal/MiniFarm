using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public enum ProductType
{
    Hay,
    Flour,
    Bread
}

[CreateAssetMenu(fileName = "Product", menuName = "Production/Product")]
public class Product : ScriptableObject
{
    public Sprite sprite;
    public ProductType type;
    public int id;
}
