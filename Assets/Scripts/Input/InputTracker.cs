using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTracker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        IClickable target = null;
        if(Physics.Raycast(ray, out var hit, 300f))
        {
            target = hit.transform.GetComponent<IClickable>();
            target?.OnClick();
        }
        Managers.EventManager.Instance.ONOnClick(target);
    }
}
