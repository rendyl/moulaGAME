using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI gameOVER;
    public TextMeshProUGUI restartText;

    public ParticleSystem dmgParticles;
    public AudioSource dmgSound;
    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        deathSound.Stop();
        dmgSound.Stop();
        dmgParticles.Stop();
        currentHealth = maxHealth;
        setHPText();
        gameOVER.SetText("");
    }

    void setHPText()
    {
        hpText.text = "HP : " + currentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upHP(int health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        setHPText();
    }

    public void hurtPlayer(int damage)
    {
        if(currentHealth > 0)
        {
            dmgSound.Play();
            dmgParticles.Play();
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                deathSound.Play();
                gameOVER.SetText("GAME OVER");
                restartText.gameObject.SetActive(true);
                currentHealth = 0;
                gameObject.GetComponent<PlayerController>().alive = false;
                gameObject.GetComponent<PlayerController>().myGun.isFiring = false;
            }
            setHPText();
        }
    }
}
