using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;

    public float lifeTime;
    public int dmg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(dmg, other.contacts[0].point);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Obstacle") Destroy(gameObject);
    }
}
