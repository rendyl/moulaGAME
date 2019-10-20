using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    public float baseMoveSpeed = 7;

    float timeBonusHP = 0;
    float timeMaxBonusHP = 0;

    float timeBonusMS = 0;
    float timeMaxBonusMS = 0;

    public bool isInvincible = false;
    float timeBonusINV = 0;
    float timeMaxBonusINV = 0;

    float timeBonusFR = 0;
    float timeMaxBonusFR = 0;

    private Rigidbody myRigidBody;
    public float kills;
    public float score;

    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusMSText;
    public TextMeshProUGUI bonusASText;
    public TextMeshProUGUI enEspritText;

    public GameObject particleHP;
    public GameObject particleINV;
    public GameObject particleMS;
    public GameObject particleAS;

    bool rotated = false;
    public bool alive = true;
    public int indexActive = 3;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
	
	private Camera mainCamera;
    public List<GunController> gunList = new List<GunController>();
    public GunController myGun;

    public void invincible(float timeBonus)
    {
        particleINV.SetActive(false);
        particleINV.SetActive(true);
        enEspritText.transform.position = new Vector3(160, 380f, 0);
        enEspritText.SetText("EN ESPRIT");
        isInvincible = true;
        timeBonusINV = 0;
        timeMaxBonusINV = timeBonus;
    }

    public void upSpeed(float msToAdd, float timeBonus)
    {
        timeBonusMS = 0;
        if (moveSpeed == baseMoveSpeed)
        {
            particleMS.SetActive(true);
            bonusMSText.SetText("MS+"); 
            moveSpeed += msToAdd;
        }
        timeMaxBonusMS = timeBonus;
    }

    public void upHP(float timeBonus)
    {
        particleHP.SetActive(true);
        timeBonusHP = 0;
        timeMaxBonusHP = timeBonus;
    }

    public void upFireRate(float multiplier, float timeBonus)
    {
        float fireRate = GetComponentInChildren<GunController>().baseTimeBetweenShots;
        float baseFireRate = GetComponentInChildren<GunController>().timeBetweenShots;

        timeBonusFR = 0;
        if (fireRate == baseFireRate)
        {
            particleAS.SetActive(true);
            bonusASText.SetText("AS+");
            GetComponentInChildren<GunController>().timeBetweenShots /= multiplier;
        }

        timeMaxBonusFR = timeBonus;
    }

    void setSCOREText()
    {
        score = kills * 50;
        scoreText.SetText("MOULAGA : {0} $", score);
    }

    // Start is called before the first frame update
    void Start()
    {
        myGun = gunList[indexActive];
        myGun.gameObject.SetActive(true);
        weaponText.SetText(myGun.weaponName);
        enEspritText.SetText("");
        bonusMSText.SetText("");
        bonusASText.SetText("");
        kills = 0;
        setSCOREText();
        myRigidBody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        if(alive) myRigidBody.velocity = moveVelocity;
        else if (!rotated)
        {
            myRigidBody.velocity = new Vector3(0, 0, 0); 
            rotated = true;
            transform.Rotate(0, 0, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setSCOREText();

        if (timeBonusHP < timeMaxBonusHP) timeBonusHP += Time.deltaTime;
        if (timeBonusHP > timeMaxBonusHP)
        {
            particleHP.SetActive(false);
            timeBonusHP = 0;
            timeMaxBonusHP = 0;
        }

        if (timeBonusMS < timeMaxBonusMS) timeBonusMS += Time.deltaTime;
        if (timeBonusMS > timeMaxBonusMS && moveSpeed != baseMoveSpeed)
        {
            particleMS.SetActive(false);
            bonusMSText.SetText("");
            timeBonusMS = 0;
            timeMaxBonusMS = 0;
            moveSpeed = baseMoveSpeed;
        }

        if (timeBonusINV < timeMaxBonusINV) timeBonusINV += Time.deltaTime;
        if (timeBonusINV > 0)
        {
            enEspritText.transform.position += 50 * timeBonusINV * new Vector3(1,0,0);
        }
        if (timeBonusINV > timeMaxBonusINV)
        {
            enEspritText.transform.position = new Vector3(-100, 0, 0);
            enEspritText.SetText("");
            particleINV.SetActive(false);
            isInvincible = false;
            timeBonusINV = 0;
            timeMaxBonusINV = 0;
        }


        if (timeBonusFR < timeMaxBonusFR) timeBonusFR += Time.deltaTime;
        float fireRate = GetComponentInChildren<GunController>().baseTimeBetweenShots;
        float baseFireRate = GetComponentInChildren<GunController>().timeBetweenShots;

        if (timeBonusFR > timeMaxBonusFR && fireRate != baseFireRate)
        {
            particleAS.SetActive(false);
            bonusASText.SetText("");
            timeBonusFR = 0;
            timeMaxBonusFR = 0;
            GetComponentInChildren<GunController>().timeBetweenShots = GetComponentInChildren<GunController>().baseTimeBetweenShots;
        }


        if(alive)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * moveSpeed;

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            if(Input.mouseScrollDelta.y != 0)
            {
                bool actualState = myGun.isFiring;
                myGun.timeBetweenShots = myGun.baseTimeBetweenShots;
                myGun.isFiring = false;
                gunList[indexActive].gameObject.SetActive(false);
                indexActive = (indexActive + (int)Input.mouseScrollDelta.y) % (int)gunList.Count;
                if(indexActive < 0) indexActive = (int)gunList.Count - 1;
                gunList[indexActive].gameObject.SetActive(true);
                myGun = gunList[indexActive];
                weaponText.SetText(myGun.weaponName);
                myGun.isFiring = actualState;
                if(timeBonusFR < timeMaxBonusFR) myGun.timeBetweenShots /= 2;
            }

            if(myGun.shootingType == "semiAuto")
            {
               myGun.isFiring = false;
            }
            else if (myGun.shootingType == "auto")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    myGun.isFiring = false;
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                myGun.isFiring = true;
            }    
        }
        else
        {
            myRigidBody.velocity = new Vector3(0, 0, 0); 
        }
    }
}
