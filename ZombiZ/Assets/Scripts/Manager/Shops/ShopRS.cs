using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRS : ShopManager
{
    public int levelMax = 5;
    int levelUpgrade = 1;
    public float rsMultiplier = 0.9f;

    private void Start()
    {
        nomProduit = "RELOADING SPEED LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if (levelUpgrade < levelMax)
        { 
            client.GetComponent<PlayerController>().updateReloadSpeed(rsMultiplier);
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "RELOADING SPEED LEVEL " + levelUpgrade;
        }

        if (levelUpgrade == levelMax)
        {
            price = 0;
            canBuy = false;
            nomProduit = "RELOADING SPEED LEVEL MAX";
        }
    }
}
