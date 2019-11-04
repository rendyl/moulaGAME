using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody myRigidBody;
    public float moveSpeed = 2;
    public float maxSpeed = 2;
    public int dmg;

    public bool alive = true;
    bool playerInSight;
    bool enLightened;

    public float distanceToChase = 20;

    public PlayerController playerToChase;
    public ObstacleFactoryController obsController;

    // Start is called before the first frame update
    void Start()
    {
        //playerInSight = false;
        myRigidBody = GetComponent<Rigidbody>();
        playerToChase = FindObjectOfType<PlayerController>();
        obsController = FindObjectOfType<ObstacleFactoryController>();

        myRigidBody.velocity = new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3));
    }

    float distance(GameObject go)
    {
        return Mathf.Sqrt((transform.position.x - go.transform.position.x) * (transform.position.x - go.transform.position.x) + (transform.position.z - go.transform.position.z) * (transform.position.z - go.transform.position.z));
    }

    void moveCloserToBoids(List<GameObject> boids, float diviseur)
    {
        float avgX = 0;
        float avgZ = 0;

        foreach (GameObject boid in boids)
        {
            avgX += transform.position.x - boid.transform.position.x;
            avgZ += transform.position.z - boid.transform.position.z;
        }
        if (boids.Count != 0)
        {
            avgX /= boids.Count;
            avgZ /= boids.Count;
        }

        myRigidBody.velocity -= new Vector3(avgX / diviseur, 0, avgZ / diviseur);
    }

    void moveCloserToPlayer(float diviseur)
    {
        float avgX = gameObject.transform.position.x - playerToChase.gameObject.transform.position.x;
        float avgZ = gameObject.transform.position.z - playerToChase.gameObject.transform.position.z;

        myRigidBody.velocity -= new Vector3(avgX / diviseur, 0, avgZ / diviseur);
    }

    void moveWithBoids(List<GameObject> boids, float diviseur)
    {
        float avgX = 0;
        float avgZ = 0;

        foreach (GameObject boid in boids)
        {
            avgX += boid.GetComponent<Rigidbody>().velocity.x;
            avgZ += boid.GetComponent<Rigidbody>().velocity.z;
        }
        if(boids.Count != 0)
        {
            avgX /= boids.Count;
            avgZ /= boids.Count;
        }
        
        myRigidBody.velocity += new Vector3(avgX / diviseur, 0, avgZ / diviseur);
    }

    void moveAwayFromBoids(List<GameObject> boids, float minDistance, float diviseur)
    {
        float distX = 0;
        float distZ = 0;
        int nbClose = 0;

        foreach (GameObject boid in boids)
        {
            float dist = distance(boid);
            if (dist < minDistance)
            {
                nbClose++;

                float xDiff = (transform.position.x - boid.transform.position.x);
                float zDiff = (transform.position.z - boid.transform.position.z);

                if (xDiff >= 0)
                {
                    xDiff = Mathf.Sqrt(minDistance) - xDiff;
                }
                else
                {
                    xDiff = - Mathf.Sqrt(minDistance) - xDiff;
                }

                if (zDiff >= 0)
                {
                    zDiff = Mathf.Sqrt(minDistance) - zDiff;
                }
                else 
                {  
                    zDiff = - Mathf.Sqrt(minDistance) - zDiff;
                }

                distX += xDiff;
                distZ += zDiff;
            }
        }
        
        if (nbClose == 0) return;

        myRigidBody.velocity -= new Vector3(distX / diviseur, 0, distZ / diviseur);
    }

    void moveAwayFromObstacles(List<GameObject> obstacles, float minDistance, float diviseur)
    {
        float distX = 0;
        float distZ = 0;
        int nbClose = 0;

        foreach (GameObject obs in obstacles)
        {
            float dist = distance(obs);
            if (dist < minDistance)
            {
                nbClose++;

                float xDiff = (transform.position.x - obs.transform.position.x);
                float zDiff = (transform.position.z - obs.transform.position.z);

                if (xDiff >= 0)
                {
                    xDiff = Mathf.Sqrt(minDistance) - xDiff;
                }
                else
                {
                    xDiff = -Mathf.Sqrt(minDistance) - xDiff;
                }

                if (zDiff >= 0)
                {
                    zDiff = Mathf.Sqrt(minDistance) - zDiff;
                }
                else
                {
                    zDiff = -Mathf.Sqrt(minDistance) - zDiff;
                }

                distX += xDiff;
                distZ += zDiff;
            }
        }

        if (nbClose == 0) return;

        myRigidBody.velocity -= new Vector3(distX / diviseur, 0, distZ / diviseur);
    }

    void moveAwayFromPlayer(float minDistance)
    {
        float distX = 0;
        float distZ = 0;
        int nbClose = 0;

        float dist = distance(playerToChase.gameObject);
        if (dist < minDistance)
        {
            nbClose++;

            float xDiff = (transform.position.x - playerToChase.gameObject.transform.position.x);
            float zDiff = (transform.position.z - playerToChase.gameObject.transform.position.z);

            if (xDiff >= 0)
            {
                xDiff = Mathf.Sqrt(minDistance) - xDiff;
            }
            else
            {
                xDiff = -Mathf.Sqrt(minDistance) - xDiff;
            }

            if (zDiff >= 0)
            {
                zDiff = Mathf.Sqrt(minDistance) - zDiff;
            }
            else
            {
                zDiff = -Mathf.Sqrt(minDistance) - zDiff;
            }

            distX += xDiff;
            distZ += zDiff;
        }

        if (nbClose == 0) return;

        myRigidBody.velocity -= new Vector3(distX / 5, 0, distZ / 5);
    }

    private void move(float multiplieur)
    {
        if (Mathf.Abs(myRigidBody.velocity.x) > maxSpeed || Mathf.Abs(myRigidBody.velocity.z) > maxSpeed)
        {
            myRigidBody.velocity = myRigidBody.velocity.normalized * multiplieur * moveSpeed;
        }
        transform.forward = myRigidBody.velocity;
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            List<GameObject> boids = gameObject.GetComponentInParent<ZombieFactoryController>().listZombies;
            List<GameObject> obstacles = obsController.listObstacles;

            List<GameObject> closeBoids = new List<GameObject>();

            foreach (GameObject otherBoid in boids)
            {
                if (otherBoid == gameObject)
                {

                }
                else
                {
                    float dist = distance(otherBoid);
                    if (dist < 200) closeBoids.Add(otherBoid);
                }
            }

            // Machine a etats :

            //if (playerInSight || boids.Count == 1)
            //{
                if (playerToChase.GetComponent<PlayerController>().isInvincible) moveAwayFromPlayer(3f);
                else moveCloserToPlayer(1f);
            //}

            moveAwayFromObstacles(obstacles, 3f, 0.5f);

            if (enLightened)
            {
                moveSpeed = 2;
            }
            else
            {
                moveSpeed = maxSpeed;
            }

            // else if (playerToChase.state != "weak") moveAwayFromPlayer(5);


            moveCloserToBoids(closeBoids, 10);
            moveWithBoids(closeBoids, 10);
            moveAwayFromBoids(closeBoids, 1f, 1);

            move(2);
        }
        else
        {
            myRigidBody.velocity = new Vector3(0, -1, 0);
        }
    }

    void Update()
    {
        if(alive)
        {
            if (GetComponent<FieldOfView>().visibleTargets.Count > 0 && playerToChase.alive) playerInSight = true;
            else playerInSight = false;

            if (playerToChase.gameObject.GetComponent<FieldOfView>().visibleTargets.Contains(transform)) enLightened = true;
            else enLightened = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().isInvincible)
            {
                gameObject.GetComponent<EnemyHealthManager>().currentHealth = 0;
            }
            else
            {
                other.gameObject.GetComponent<PlayerHealthManager>().hurtPlayer(dmg);   
            }
        }
    }
}