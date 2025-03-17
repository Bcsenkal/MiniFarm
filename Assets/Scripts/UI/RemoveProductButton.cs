using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveProductButton : ProductionButton
{
    protected override void Awake()
    {
        base.Awake();
        button.onClick.AddListener(RemoveProduction);
    }

    protected void RemoveProduction()
    {
        currentSelection.RemoveProductionFromQueue();
    }

    protected override void SetSelection(Building building)
    {
        base.SetSelection(building);
        if(currentSelection == null) return;
        button.interactable = currentSelection.HasQueue();
    }
}
