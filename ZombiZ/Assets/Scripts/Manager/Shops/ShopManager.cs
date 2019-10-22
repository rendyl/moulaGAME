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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textSHOP.SetText("F : ACHETER " + nomProduit + "\nPOUR " + price + "$");
            if(cdActuel >= shopCD)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cdActuel = 0;
                    if (client.moulaga >= price)
                    {
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