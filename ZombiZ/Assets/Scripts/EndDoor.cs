using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDoor : Door
{
    public TextMeshProUGUI gameOVER;
    public TextMeshProUGUI restartText;

    bool isEnded = false;
    public BonusFactory bonusInstantiator;
    public ZombieFactoryController zombieInstantiator;

    public override void shopping()
    {
        gameOVER.SetText("YOU WON");
        restartText.gameObject.SetActive(true);
        restartText.SetText("MERCI D'AVOIR JOUE AU JEU ! \n APPUYEZ SUR ENTREE POUR REJOUER");
        bonusInstantiator.spawnNukeAtPosition(client.gameObject.transform.position);
        zombieInstantiator.textBeforeRound.enabled = false;
        zombieInstantiator.enabled = false;
        textSHOP.SetText("");
        isEnded = true;
    }

    public void FixedUpdate()
    {
        if(isEnded)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
