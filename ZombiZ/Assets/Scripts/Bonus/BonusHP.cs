using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHP : Bonus
{
    public override void activateBonus()
    {
        player.GetComponent<PlayerHealthManager>().upHP(50);
    }
}
