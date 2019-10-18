using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMS : Bonus
{
    public override void activateBonus()
    {
        player.GetComponent<PlayerController>().upSpeed(3, 10);
    }
}
