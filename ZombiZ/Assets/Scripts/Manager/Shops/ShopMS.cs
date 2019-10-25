using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMS : ShopManager
{
    int levelUpgrade = 1;
    public float msMultiplier = 1.05f;

    private void Start()
    {
        nomProduit = "MOVE SPEED LEVEL " + levelUpgrade;
    }

    public override void shopping()
    {
        if (levelUpgrade < 5)
        {
            client.GetComponent<PlayerController>().updateMoveSpeed(msMultiplier);
            levelUpgrade++;
            price = price * levelUpgrade;
            nomProduit = "MOVE SPEED LEVEL " + levelUpgrade;
        }

        if (levelUpgrade == 5)
        {
            price = 0;
            nomProduit = "MOVE SPEED LEVEL MAX";
        }
    }
}
