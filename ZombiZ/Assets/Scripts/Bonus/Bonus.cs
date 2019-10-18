using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public PlayerController player;
    public float timeToLive = 10f;
    public float timeLiving;
    private float rotationSpeed = 2.5f;
    private float rotationMaxSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        timeLiving = 0f;
        player = FindObjectOfType<PlayerController>();
    }

    void rotateBonus()
    {
        if (timeLiving <= 0.95f * timeToLive)
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0), 45 * rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0), 45 * rotationMaxSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLiving += Time.deltaTime;

        rotateBonus();     

        if (timeLiving > timeToLive)
        {
            gameObject.GetComponentInParent<BonusFactory>().removeFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void activateBonus()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController>().alive)
        {
            gameObject.GetComponentInParent<BonusFactory>().removeFromList(gameObject);
            activateBonus();
            Destroy(gameObject);
        }
        
    }
}
