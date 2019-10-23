﻿using System.Collections;
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

    public void spawnBonusAtPosition(Vector3 position)
    {
        int dice = Random.Range(0, 115);
        GameObject gameObjectToInstantiate;
        if (dice < 25)
        {
            gameObjectToInstantiate = bonusMSPrefab;
        }
        else if (25 <= dice && dice < 50)
        {
            gameObjectToInstantiate = bonusHPPrefab;
        }
        else if (50 < dice && dice <= 75)
        {
            gameObjectToInstantiate = bonusFRPrefab;
        }
        else if (75 < dice && dice <= 95)
        {
            gameObjectToInstantiate = bonusNUKEPrefab;
        }
        else
        {
            gameObjectToInstantiate = bonusINVPrefab;
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
                int dice = Random.Range(0, 105);
                GameObject gameObjectToInstantiate;
                if (dice < 33)
                {
                    gameObjectToInstantiate = bonusMSPrefab;
                }
                else if (33 <= dice && dice < 66)
                {
                    gameObjectToInstantiate = bonusHPPrefab;
                }
                else if (66 < dice && dice <= 99)
                {
                    gameObjectToInstantiate = bonusFRPrefab;
                }
                else if (99 < dice && dice <= 102)
                {
                    gameObjectToInstantiate = bonusNUKEPrefab;
                }
                else
                {
                    gameObjectToInstantiate = bonusINVPrefab;
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
        
    }
}
