using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNUKE : Bonus
{
    public override void activateBonus()
    {
        ZombieFactoryController zController = FindObjectOfType<ZombieFactoryController>();
        foreach (GameObject zombie in zController.listZombies)
        {
        	zombie.GetComponent<EnemyHealthManager>().currentHealth = 0;
        }
    }
}
