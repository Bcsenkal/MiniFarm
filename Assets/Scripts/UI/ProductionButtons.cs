using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionButtons : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Managers.EventManager.Instance.OnClick += ShowButtons;
    }

    private void ShowButtons(IClickable clickable) 
    {
        if(clickable == null)
        {
            transform.localScale = Vector3.zero;
            return;
        }
        if((clickable as Building).GetBuildingType() == BuildingType.Generator)
        {
            transform.localScale = Vector3.zero;
            return;
        }
        transform.position = cam.WorldToScreenPoint((clickable as MonoBehaviour).transform.position + Vector3.up * 5f);
    }
}
