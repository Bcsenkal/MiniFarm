using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatFactory : Building
{
    public override void OnClick()
    {
        IsSelected = true;

    }

    public override void CloseUIElements(IClickable clickable)
    {
        base.CloseUIElements(clickable);
    }
}
