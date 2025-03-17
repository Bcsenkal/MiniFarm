using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionButton : MonoBehaviour
{
    protected Building currentSelection;
    protected Button button;
    protected TextMeshProUGUI buttonText;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    protected virtual void Start()
    {
        Managers.EventManager.Instance.OnBuildingSelect += SetSelection;
        Managers.EventManager.Instance.OnProductAmountChange += OnProductAmountChange;
        Managers.EventManager.Instance.OnProductionQueueChange += OnProductionQueueChange;
        Managers.EventManager.Instance.OnProductionComplete += OnProductionComplete;
    }

    protected virtual void SetSelection(Building building)
    {
        currentSelection = building.GetBuildingType() == BuildingType.Generator ? null : building;
    }

    protected virtual void OnProductAmountChange(ProductType productType, int amount)
    {
        if(currentSelection == null) return;
        if(currentSelection.GetBuildingType() == BuildingType.Generator) return;
        SetSelection(currentSelection);
    }

    protected virtual void OnProductionQueueChange()
    {
        SetSelection(currentSelection);
    }

    protected virtual void OnProductionComplete()
    {
        if(currentSelection == null) return;
        if(currentSelection.GetBuildingType() == BuildingType.Generator) return;
        SetSelection(currentSelection);
    }
}
