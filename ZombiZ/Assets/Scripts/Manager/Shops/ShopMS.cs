﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMS : ShopManager
{
    public int levelMax = 5;
    int levelUpgrade = 1;
    public float msMultiplier = 1.05f;

    private void Start()
    {
        nomProduit = "MOVE SPEED LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if (levelUpgrade < levelMax)
        {
            client.GetComponent<PlayerController>().updateMoveSpeed(msMultiplier);
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "MOVE SPEED LEVEL " + levelUpgrade;
        }

        if (levelUpgrade == levelMax)
        {
            price = 0;
            canBuy = false;
            nomProduit = "MOVE SPEED LEVEL MAX";
        }
    }
}
