using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour
{
    public GameObject bonusHPPrefab;
    public GameObject bonusMSPrefab;
    public GameObject bonusINVPrefab;
    public GameObject bonusFRPrefab;
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

    // Update is called once per frame
    void Update()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime)
        {
            if (listBonus.Count < nbMaxBonus)
            {
                int dice = Random.Range(0, 11);
                GameObject gameObjectToInstantiate;
                if (dice <= 2)
                {
                    gameObjectToInstantiate = bonusMSPrefab;
                }
                else if (3 <= dice && dice <= 5)
                {
                    gameObjectToInstantiate = bonusHPPrefab;
                }
                else if (6 <= dice && dice <= 8)
                {
                    gameObjectToInstantiate = bonusFRPrefab;
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
}
