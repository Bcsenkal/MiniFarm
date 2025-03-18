using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
public class ProductionInfo : MonoBehaviour
{
    private StringBuilder stringBuilder = new StringBuilder();
    private Building building;
    private bool isGenerator;
    private Camera cam;

    private TextMeshProUGUI currentAmountText;
    private Slider progressFill;
    private TextMeshProUGUI progressText;
    private TextMeshProUGUI capacityText;
    private Image productIcon;

    void Awake()
    {
        building = transform.GetComponentInParent<Building>();
        isGenerator = building.GetBuildingType() == BuildingType.Generator;
        currentAmountText = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        progressFill = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        progressText = progressFill.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        productIcon = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        productIcon.sprite = building.GetBuildingData().outputProduct.sprite;
        if(isGenerator) return;
        capacityText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        transform.localScale = Vector3.zero;
        
    }

    void Start()
    {
        cam = Camera.main;
        transform.LookAtCamera(cam);
        Invoke(nameof(CheckVisibility),0.1f);
    }

    private void CheckVisibility()
    {
        CheckProduction(building.HasQueue(),building.CanGetHarvested());
    }

    public void CheckProduction(bool hasQueue,bool canGetHarvested)
    {
        transform.localScale = hasQueue || canGetHarvested ? Vector3.one : Vector3.zero;
    }

    public void UpdateTime(float timeLeft, float timeToProduce)
    {
        if(timeLeft < -0.9f)
        {
            progressText.text = "";
            progressFill.value = 0;
            return;
        }
        progressFill.value = 1 - timeLeft / timeToProduce;
        stringBuilder.Clear();
        stringBuilder.Append(Mathf.CeilToInt(timeLeft));
        stringBuilder.Append(" s");
        progressText.text = stringBuilder.ToString();
    }

    public void UpdateAmount(int currentAmount,int qeuedAmount)
    {
        currentAmountText.text = currentAmount.ToString();
        if(isGenerator) return;
        
        stringBuilder.Clear();
        stringBuilder.Append(qeuedAmount + currentAmount);
        stringBuilder.Append("/");
        stringBuilder.Append(building.GetBuildingData().capacity);
        capacityText.text = stringBuilder.ToString();
    }

    public void IsFull()
    {
        progressText.text = "FULL";
        progressFill.value = 1;
    }
}
