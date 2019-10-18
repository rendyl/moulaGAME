using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusINV : Bonus
{
    public override void activateBonus()
    {
        player.GetComponent<PlayerController>().invincible(5f);
    }
}
