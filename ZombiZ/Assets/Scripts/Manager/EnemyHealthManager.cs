﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float timeBeforeDeath = 1f;
    public float timeToDie = 0f;
    public float timePartPlay = 5f;
    public int maxHealth;
    public int killValue;
    public float currentHealth;
    bool rotated = false;
    public PlayerController playerToChase;
    public BonusFactory bonusInstantiator;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
        bonusInstantiator = FindObjectOfType<BonusFactory>();
        playerToChase = FindObjectOfType<PlayerController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        timePartPlay += Time.deltaTime;
        if(timePartPlay > timeBeforeDeath && timeToDie <= 0)
        {
            ps.Stop();
        }
        
        if(timeToDie > 0 && !rotated)
        {
            rotated = true;
            playerToChase.moulaga += 50 * killValue;
            playerToChase.kills++;
            transform.Rotate(0, 0, 90);
        }

        if(timeBeforeDeath < timeToDie)
        {
            int dice = Random.Range(0, 5);
            if (dice == 1) bonusInstantiator.spawnBonusAtPosition(gameObject.transform.position);
            ps.Stop();
            gameObject.GetComponentInParent<ZombieFactoryController>().removeFromList(gameObject);      
            Destroy(gameObject);
        }

        if(currentHealth <= 0)
        {
            timeToDie += Time.deltaTime;
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            gameObject.GetComponent<EnemyController>().alive = false; 
        }
    }

    public void hurtEnemy(float damage, Vector3 pointHit)
    {
        if(currentHealth > 0)
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            playerToChase.moulaga += 10;
            ps.transform.position = pointHit;
            ps.Play();
            timePartPlay = 0f;
            currentHealth -= damage;
        } 
    }
}
