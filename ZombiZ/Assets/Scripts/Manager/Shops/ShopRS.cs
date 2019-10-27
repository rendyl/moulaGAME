using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRS : ShopManager
{
    int levelUpgrade = 1;
    public float rsMultiplier = 0.9f;

    private void Start()
    {
        nomProduit = "RELOADING SPEED LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if (levelUpgrade < 5)
        { 
            client.GetComponent<PlayerController>().updateReloadSpeed(rsMultiplier);
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "RELOADING SPEED LEVEL " + levelUpgrade;
        }

        if (levelUpgrade == 5)
        {
            price = 0;
            canBuy = false;
            nomProduit = "RELOADING SPEED LEVEL MAX";
        }
    }
}
