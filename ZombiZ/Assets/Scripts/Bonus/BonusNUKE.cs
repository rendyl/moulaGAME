using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNUKE : Bonus
{
    nukeEffectsManager nEf;

    void Start()
    {
        nEf = FindObjectOfType<nukeEffectsManager>();
    }

    public override void activateBonus()
    {
        ZombieFactoryController zController = FindObjectOfType<ZombieFactoryController>();
        foreach (GameObject zombie in zController.listZombies)
        {
        	zombie.GetComponent<EnemyHealthManager>().currentHealth = 0;
        }

        nEf.activer();
    }
}
