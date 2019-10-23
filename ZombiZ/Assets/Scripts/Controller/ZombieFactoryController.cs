using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactoryController : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject zombieBIGPrefab;
    public GameObject zombieRICHPrefab;

    public float reloadTime = 2f;
    public float reloadProgress = 0f;
    public int nbMaxZombies = 20;
    public int nbZombieBase = 5;
    public List<GameObject> listZombies;
    public ParticleSystem nukeParticles;

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
        if(!nukeParticles.isEmitting)
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

                    zombie.transform.parent = gameObject.transform;
                    listZombies.Add(zombie);
                    reloadProgress = 0;
                }
            }
        }      
    }
}
