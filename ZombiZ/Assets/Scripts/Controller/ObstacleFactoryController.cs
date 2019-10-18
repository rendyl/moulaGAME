using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactoryController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public List<GameObject> listObstacles;
    public int nbObstacles = 10;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nbObstacles; i++)
        {
            GameObject obstacle = Instantiate<GameObject>(obstaclePrefab, new Vector3(Random.Range(-20, 20), 2.5f, Random.Range(-20, -5)), Quaternion.identity);
            obstacle.transform.parent = gameObject.transform;
            listObstacles.Add(obstacle);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
