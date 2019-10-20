using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public AudioSource gunShootSound;

    public string weaponName;
    public string shootingType;
    public string weaponType;
    public bool isFiring;
    public BulletController bullet;

    public int bulletDMG;

    public float bulletSpeed;
    public float baseBulletSpeed;

    public float timeBetweenShots;
    public float baseTimeBetweenShots;
    public float shotCounter;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Mitraillette Pistolet
    public void shootPattern()
    {
        if (weaponType == "shotgun")
        {
            for(int i = 0; i < 5; i++)
            { 
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, +10f - i * 5f)) as BulletController;
                newBullet.speed = bulletSpeed;
                newBullet.type = "normal";
                newBullet.dmg = bulletDMG;
            }  
        }

        if (weaponType == "rifle")
        {
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletSpeed;
            newBullet.type = "normal";
            newBullet.dmg = bulletDMG;
        }

        if (weaponType == "famas")
        {
            for(int i = 0; i < 3; i++)
            {
                BulletController newBullet = Instantiate(bullet, firePoint.position + firePoint.up*i/3, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                newBullet.type = "normal"; 
                newBullet.dmg = bulletDMG;
            }
        }

        if (weaponType == "sniper")
        {
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletSpeed;
            newBullet.type = "perforing";
            newBullet.dmg = bulletDMG;
        }       
    } 

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {
            if(shotCounter <= 0)
            {
                gunShootSound.Play();
                GetComponentInChildren<Light>().enabled = true;
                shotCounter = timeBetweenShots;
                
                shootPattern();
            }
            else if(shotCounter < timeBetweenShots/2)
            {
                GetComponentInChildren<Light>().enabled = false;
            }
        }
        else if (shootingType == "auto")
        {
            GetComponentInChildren<Light>().enabled = false;
            shotCounter = 0;
        }
        else if (shootingType == "semiAuto" && shotCounter < timeBetweenShots - 0.05f)
        {
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}
