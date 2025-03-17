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
    void Start()
    {
        productIcon = transform.GetChild(0).GetComponent<Image>();
        productAmount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        productIcon.sprite = product.sprite;
        Managers.EventManager.Instance.OnProductAmountChange += OnProductAmountChange;
        productAmount.text = ResourceManager.Instance.GetProductAmount(product.type).ToString();
    }

    private void OnProductAmountChange(ProductType type, int amount)
    {
        if(type != product.type) return;
        productAmount.text = amount.ToString();
    }


}
