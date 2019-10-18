using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        setHPText();
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.GetComponent<PlayerController>().alive = false;
            gameObject.GetComponent<PlayerController>().myGun.isFiring = false;
        }
        setHPText();
    }
}
