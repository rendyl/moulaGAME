using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour
{
    public GameObject bonusHPPrefab;
    public GameObject bonusMSPrefab;
    public GameObject bonusINVPrefab;
    public GameObject bonusFRPrefab;
    public GameObject bonusNUKEPrefab;
    public float reloadTime = 2f;
    public float reloadProgress = 0f;
    public int nbMaxBonus = 1;
    public List<GameObject> listBonus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void removeFromList(GameObject go)
    {
        listBonus.Remove(go);
    }

    public void spawnNukeAtPosition(Vector3 position)
    {
        GameObject bonus = Instantiate<GameObject>(bonusNUKEPrefab, position, Quaternion.identity);
        bonus.transform.parent = gameObject.transform;
        listBonus.Add(bonus);
    }

    public void spawnBonusAtPosition(Vector3 position)
    {
        int dice = Random.Range(0, 101);
        GameObject gameObjectToInstantiate;
        if (0 <= dice && dice < 30)
        {
            gameObjectToInstantiate = bonusMSPrefab;
        }
        else if (60 <= dice && dice < 90)
        {
            gameObjectToInstantiate = bonusFRPrefab;
        }
        else if (90 <= dice && dice <= 95)
        {
            gameObjectToInstantiate = bonusNUKEPrefab;
        }
        else if (95 < dice && dice <= 100)
        {
            gameObjectToInstantiate = bonusINVPrefab;
        }
        else
        {
            gameObjectToInstantiate = bonusHPPrefab;
        }
        GameObject bonus = Instantiate<GameObject>(gameObjectToInstantiate, position, Quaternion.identity);
        bonus.transform.parent = gameObject.transform;
        listBonus.Add(bonus);
    }

    public void spawnBonusWithCD()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime)
        {
            if (listBonus.Count < nbMaxBonus)
            {
                int dice = Random.Range(0, 101);
                Debug.Log(dice);
                GameObject gameObjectToInstantiate;
                if (0 <= dice && dice < 33)
                {
                    gameObjectToInstantiate = bonusMSPrefab;
                }
                else if (66 < dice && dice < 98)
                {
                    gameObjectToInstantiate = bonusFRPrefab;
                }
                else if (dice == 98)
                {
                    gameObjectToInstantiate = bonusNUKEPrefab;
                }
                else if (dice == 99)
                {
                    gameObjectToInstantiate = bonusINVPrefab;
                }
                else
                {
                    gameObjectToInstantiate = bonusHPPrefab;
                }
                GameObject bonus = Instantiate<GameObject>(gameObjectToInstantiate, new Vector3(Random.Range(-10, 10), 1, Random.Range(5, 10)), Quaternion.identity);
                bonus.transform.parent = gameObject.transform;
                listBonus.Add(bonus);
                reloadProgress = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameObject bonus = Instantiate<GameObject>(bonusINVPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            bonus.transform.parent = gameObject.transform;
            listBonus.Add(bonus);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject bonus = Instantiate<GameObject>(bonusNUKEPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            bonus.transform.parent = gameObject.transform;
            listBonus.Add(bonus);
        }
        */
        // spawnBonusWithCD();   
    }
}
