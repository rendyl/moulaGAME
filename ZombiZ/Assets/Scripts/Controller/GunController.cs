using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public AudioSource gunShootSound;

    public bool isFiring;
    public BulletController bullet;
    public float bulletSpeed;
    public float baseBulletSpeed;

    public float timeBetweenShots;
    public float baseTimeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                gunShootSound.Play();
                GetComponentInChildren<Light>().enabled = true;
                shotCounter = timeBetweenShots;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
            }
            else if(shotCounter < timeBetweenShots/2)
            {

                GetComponentInChildren<Light>().enabled = false;
            }
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
            shotCounter = 0;
        }
    }
}
