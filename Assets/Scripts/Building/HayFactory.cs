using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayFactory : Building
{

    public override void OnClick()
    {
        Debug.Log("hay factory clicked");
        productionHandler.Harvest();
    }

}
