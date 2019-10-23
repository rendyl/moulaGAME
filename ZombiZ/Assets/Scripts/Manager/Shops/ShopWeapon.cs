using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : ShopManager
{
    public GameObject weaponToSell;
    public int priceChargeur;

    private void Start()
    {
        shopCD = 0.1f;
    }

    public override void shopping()
    {
        if(client.GetComponent<PlayerController>().gunList.Contains(weaponToSell.GetComponent<GunController>()))
        {
            foreach (GunController gc in client.GetComponent<PlayerController>().gunList)
            {
                if (gc == weaponToSell.GetComponent<GunController>()) gc.nbBallesTot += gc.ballesParChargeur;
            }
        }
        else
        {
            client.GetComponent<PlayerController>().gunList.Add(weaponToSell.GetComponent<GunController>());
            nomProduit = "1 CHARGEUR DE " + nomProduit;
            price = priceChargeur;
        }
    }
}
