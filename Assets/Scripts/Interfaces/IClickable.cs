using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public bool IsSelected {get; set;}
    public void OnClick();
}
