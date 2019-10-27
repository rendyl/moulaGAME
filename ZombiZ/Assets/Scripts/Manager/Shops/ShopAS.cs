using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAS : ShopManager
{
    int levelUpgrade = 1;

    private void Start()
    {
        nomProduit = "ATTACK SPEED LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if(levelUpgrade < 5)
        {

            client.GetComponent<PlayerController>().updateATKSpeed(0.9f);
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "ATTACK SPEED LEVEL " + levelUpgrade;
        }
        
        if(levelUpgrade == 5)
        {
            price = 0;
            canBuy = false;
            nomProduit = "ATTACK SPEED LEVEL MAX";
        }
    }
}
