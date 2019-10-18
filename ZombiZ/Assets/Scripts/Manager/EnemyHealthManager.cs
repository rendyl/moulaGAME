using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float timeBeforeDeath = 1f;
    public float timeToDie = 0f;
    public int maxHealth;
    public int currentHealth;
    bool rotated = false;
    public PlayerController playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
        playerToChase = FindObjectOfType<PlayerController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if(timeToDie > 0 && !rotated)
        {
            rotated = true;
            transform.Rotate(0, 0, 90);
        }

        if(timeBeforeDeath < timeToDie)
        {
            ps.Stop();
            gameObject.GetComponentInParent<ZombieFactoryController>().removeFromList(gameObject);
            playerToChase.kills++;
            Destroy(gameObject);
        }

        if (currentHealth <= 0)
        {
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            gameObject.GetComponent<EnemyController>().alive = false;
            timeToDie += Time.deltaTime;
            // TO DO DEATH ANIMATION   
        }
    }

    public void hurtEnemy(int damage, Vector3 pointHit)
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.transform.position = pointHit;
        ps.Play();

        currentHealth -= damage;
    }
}
