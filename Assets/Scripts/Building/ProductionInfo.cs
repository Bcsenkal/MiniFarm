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

    void Awake()
    {
        building = transform.GetComponentInParent<Building>();
        isGenerator = building.GetBuildingType() == BuildingType.Generator;
        currentAmountText = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        progressFill = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        progressText = progressFill.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        if(isGenerator) return;
    }

    void Start()
    {
        cam = Camera.main;
        transform.LookAtCamera(cam);
    }

    public void UpdateTime(float timeLeft, float timeToProduce)
    {
        progressFill.value = 1 - timeLeft / timeToProduce;
        stringBuilder.Clear();
        stringBuilder.Append(Mathf.CeilToInt(timeLeft));
        stringBuilder.Append(" s");
        progressText.text = stringBuilder.ToString();
    }

    public void UpdateAmount(int currentAmount)
    {
        currentAmountText.text = currentAmount.ToString();
    }

    public void IsFull()
    {
        progressText.text = "FULL";
        progressFill.value = 1;
    }
}
