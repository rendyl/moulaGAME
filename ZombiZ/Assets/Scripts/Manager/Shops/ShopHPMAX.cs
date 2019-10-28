using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHPMAX : ShopManager
{
    public int levelMax = 6;
    int levelUpgrade = 1;
    private void Start()
    {
        nomProduit = "HP MAX LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if (levelUpgrade < levelMax)
        {
            client.GetComponent<PlayerHealthManager>().maxHealth += 20;
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "HP MAX LEVEL " + levelUpgrade;
        }

        if (levelUpgrade == levelMax)
        {
            price = 0;
            canBuy = false;
            nomProduit = "HP MAX LEVEL MAX";
        }
    }
}
