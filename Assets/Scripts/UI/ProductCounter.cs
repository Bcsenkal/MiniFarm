using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductCounter : MonoBehaviour
{
    [SerializeField]private Product product;
    private TextMeshProUGUI productAmount;
    private Image productIcon;
    private int previousAmount;
    void Start()
    {
        productIcon = transform.GetChild(0).GetComponent<Image>();
        productAmount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        productIcon.sprite = product.sprite;
        Managers.EventManager.Instance.OnProductAmountChange += ProductAmountChange;
        Managers.EventManager.Instance.OnCallProductParticle += CallProductParticle;
        previousAmount = ResourceManager.Instance.GetProductAmount(product.type);
        productAmount.text = previousAmount.ToString();

    }

    private void ProductAmountChange(ProductType type, int amount)
    {
        if(type != product.type) return;
        productAmount.text = amount.ToString();
        previousAmount = amount;
    }

    private void CallProductParticle(Product harvestedProduct, int amount)
    {
        if(harvestedProduct.type != product.type) return;
        Managers.EventManager.Instance.ONOnShowParticle(harvestedProduct,transform.position,amount);
    }



}
