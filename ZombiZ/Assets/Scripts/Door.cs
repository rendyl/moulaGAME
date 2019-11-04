using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ShopManager
{
    public override void shopping()
    {
        textSHOP.SetText("");
        Destroy(transform.parent.gameObject.transform.parent.gameObject);
    }
}
