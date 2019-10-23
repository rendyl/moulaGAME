using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;

    public string type;
    public Vector3 direction;
    public float lifeTime;
    private bool doDmg;
    public int dmg;

    // Start is called before the first frame update
    void Start()
    {
        doDmg = true;
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0.00f, Time.deltaTime * lifeTime);
        transform.Translate(direction * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;

        if (doDmg == false)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (lifeTime <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if(doDmg)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(dmg, other.contacts[0].point);
                if (type != "perforing") doDmg = false;
            }

            if (other.gameObject.tag == "Obstacle") doDmg = false;
        }
    }
}
