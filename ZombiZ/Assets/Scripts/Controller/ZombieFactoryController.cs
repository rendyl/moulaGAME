using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZombieFactoryController : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject zombieBIGPrefab;
    public GameObject zombieRICHPrefab;

    public TextMeshProUGUI textRound;
    public TextMeshProUGUI textBeforeRound;
    public float timerBeforeRound;

    public int round = 1;
    public int nbZombiesGeneratedRound = 0;
    public float reloadTime = 1f;
    public float reloadProgress = 0f;
    public int nbMaxZombies = 20;
    public int nbZombieBase = 5;
    public List<GameObject> listZombies;
    public ParticleSystem nukeParticles;

    void setTextRound()
    {
        textRound.SetText("ROUND " + round);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
        textBeforeRound.SetText("");
        timerBeforeRound = 10f;
        round = 1;
        setTextRound();
        nbZombiesGeneratedRound = 0;
    }

    public void removeFromList(GameObject go)
    {
        listZombies.Remove(go);
    }

    // Update is called once per frame
    void Update()
    {
        if (nbZombiesGeneratedRound == 0) timerBeforeRound -= Time.deltaTime;

        if (timerBeforeRound > 9f)
        {
            textBeforeRound.SetText("ROUND " + round);
        }
        else if (timerBeforeRound > 0)
        {
            textBeforeRound.SetText(((int)timerBeforeRound).ToString());
        }    

        if (timerBeforeRound < 0)
        {
            textBeforeRound.SetText("");
            if (nbZombiesGeneratedRound < 10 * round)
            {
                reloadProgress += Time.deltaTime;
                if (!nukeParticles.isEmitting)
                {
                    if (reloadProgress >= reloadTime)
                    {
                        if (listZombies.Count < nbMaxZombies)
                        {
                            int dice = Random.Range(0, 101);
                            GameObject zombie;
                            if (dice < 85) zombie = Instantiate<GameObject>(zombiePrefab, new Vector3(Random.Range(-10, 10), 1, Random.Range(5, 10)), Quaternion.identity);
                            else if (85 <= dice && dice < 95) zombie = Instantiate<GameObject>(zombieBIGPrefab, new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(5, 10)), Quaternion.identity);
                            else zombie = Instantiate<GameObject>(zombieRICHPrefab, new Vector3(Random.Range(-10, 10), 1.2f, Random.Range(5, 10)), Quaternion.identity);

                            nbZombiesGeneratedRound++;
                            zombie.transform.parent = gameObject.transform;
                            listZombies.Add(zombie);
                            reloadProgress = 0;
                        }
                    }
                }
            }
            else
            {
                if (listZombies.Count == 0)
                {
                    GetComponent<AudioSource>().pitch = 1f;
                    GetComponent<AudioSource>().Play();
                    timerBeforeRound = 10f;
                    reloadTime -= 0.2f;
                    if (reloadTime < 0.5f) reloadTime = 0.5f;
                    round++;
                    setTextRound();
                    nbZombiesGeneratedRound = 0;
                }
            }
        }       
    }
}
