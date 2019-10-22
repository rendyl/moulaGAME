using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHealth : ShopManager
{
    public override void shopping()
    {
        client.GetComponent<PlayerHealthManager>().upHP(100);
        client.GetComponent<PlayerController>().upHP(1.5f);
    }
}
