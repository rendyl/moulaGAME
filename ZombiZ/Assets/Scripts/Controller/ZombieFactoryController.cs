using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactoryController : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float reloadTime = 2f;
    public float reloadProgress = 0f;
    public int nbMaxZombies = 20;
    public int nbZombieBase = 5;
    public List<GameObject> listZombies;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nbZombieBase; i++)
        {
            GameObject zombie = Instantiate<GameObject>(zombiePrefab, new Vector3(Random.Range(-10, 10), 1, Random.Range(5, 10)), Quaternion.identity);
            zombie.transform.parent = gameObject.transform;
            listZombies.Add(zombie);
        }
    }

    public void removeFromList(GameObject go)
    {
        listZombies.Remove(go);
    }

    // Update is called once per frame
    void Update()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime)
        {
            if(listZombies.Count < nbMaxZombies)
            {
                GameObject zombie = Instantiate<GameObject>(zombiePrefab, new Vector3(Random.Range(-10, 10), 1, Random.Range(5, 10)), Quaternion.identity);
                zombie.transform.parent = gameObject.transform;
                listZombies.Add(zombie);
                reloadProgress = 0;
            }    
        }
    }
}
