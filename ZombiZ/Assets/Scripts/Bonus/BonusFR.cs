using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFR : Bonus
{
    public override void activateBonus()
    {
        player.GetComponent<PlayerController>().upFireRate(2, 10);
    }
}
