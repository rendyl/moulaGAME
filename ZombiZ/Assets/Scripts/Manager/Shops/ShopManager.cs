using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public float price;
    public string nomProduit;
    public PlayerController client;
    public TextMeshProUGUI textSHOP;
    public float shopCD = 1f;
    public float cdActuel;
    public bool canBuy = true;

    void Start()
    {
        cdActuel = shopCD;
        textSHOP.SetText("");
    }

    void Update()
    {
        if (cdActuel < shopCD) cdActuel += Time.deltaTime;
    }

    public virtual void shopping()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(canBuy) textSHOP.SetText("F : ACHETER " + nomProduit + "\nPOUR " + price + "$");
            else textSHOP.SetText("CASSE TA MERE DE LA \nJ'AI PLUS RIEN A VENDRE");

            if (Input.GetKey(KeyCode.F))
            {
                if (cdActuel >= shopCD && canBuy)
                {
                    cdActuel = 0;
                    if (client.moulaga >= price)
                    {
                        GetComponent<AudioSource>().Play();
                        client.moulaga -= price;
                        shopping();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textSHOP.SetText("");
        }
    }

}