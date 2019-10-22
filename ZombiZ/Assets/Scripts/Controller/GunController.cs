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
    public bool reload = false;

    public int nbBallesTot;
    public int ballesParChargeur;
    public int nbBallesInChargeur;

    public float reloadingTime;
    public float actualReloading;

    public float bulletSpeed;
    public float baseBulletSpeed;

    public float timeBetweenShots;
    public float baseTimeBetweenShots;
    public float shotCounter;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        nbBallesInChargeur = ballesParChargeur;
        actualReloading = reloadingTime;
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

            nbBallesInChargeur--;
        }

        if (weaponType == "rifle")
        {
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletSpeed;
            newBullet.type = "normal";
            newBullet.dmg = bulletDMG;

            nbBallesInChargeur--;
        }

        if (weaponType == "famas")
        {
            int nbBallesBurst = Mathf.Min(3, nbBallesInChargeur);
            for(int i = 0; i < nbBallesBurst; i++)
            {
                BulletController newBullet = Instantiate(bullet, firePoint.position + firePoint.up*i/3, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                newBullet.type = "normal"; 
                newBullet.dmg = bulletDMG;
            }

            nbBallesInChargeur -= nbBallesBurst;
        }

        if (weaponType == "sniper")
        {
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletSpeed;
            newBullet.type = "perforing";
            newBullet.dmg = bulletDMG;

            nbBallesInChargeur--;
        }       
    } 

    public void setReload()
    {
        if (nbBallesInChargeur < ballesParChargeur) reload = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (nbBallesInChargeur + nbBallesTot > 0)
        {
            if (actualReloading < reloadingTime)
            {
                actualReloading += Time.deltaTime;
            }
            else
            {
                if (nbBallesInChargeur <= 0 || reload)
                {
                    reload = false;
                    GetComponentInChildren<Light>().enabled = false;
                    actualReloading = 0;
                    if (nbBallesTot > 0)
                    {
                        nbBallesTot += nbBallesInChargeur;
                        nbBallesInChargeur = Mathf.Min(nbBallesTot, ballesParChargeur);
                        nbBallesTot -= nbBallesInChargeur;
                    }
                }
                else
                {
                    shotCounter -= Time.deltaTime;
                    if (isFiring)
                    {
                        if (shotCounter <= 0)
                        {
                            gunShootSound.Play();
                            GetComponentInChildren<Light>().enabled = true;
                            shotCounter = timeBetweenShots;

                            shootPattern();
                        }
                        else if (shotCounter < timeBetweenShots / 2)
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
        }
        else GetComponentInChildren<Light>().enabled = false;
    }
}