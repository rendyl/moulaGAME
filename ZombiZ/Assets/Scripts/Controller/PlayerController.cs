using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    public float baseMoveSpeed = 7;

    public float attackSpeedMultiplier = 1f;
    public float reloadSpeedMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;

    public float bonusASMultiplier = 1;
    public float bonusMSMultiplier = 1;

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
    public float moulaga;
    public float score;

    public TextMeshProUGUI magazineText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusMSText;
    public TextMeshProUGUI bonusASText;
    public TextMeshProUGUI enEspritText;

    public GameObject particleHP;
    public GameObject particleINV;
    public GameObject particleMS;
    public GameObject particleAS;

    public bool rotated = false;
    public bool alive = true;
    public int indexActive = 3;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
	
	private Camera mainCamera;
    public List<GunController> gunList = new List<GunController>();
    public GunController myGun;

    public void invincible(float timeBonus)
    {
        particleINV.GetComponent<AudioSource>().volume = 0.5f;
        particleINV.GetComponent<AudioSource>().pitch = 1f;
        particleINV.SetActive(false);
        particleINV.SetActive(true);
        enEspritText.transform.position = new Vector3(160, 380f, 0);
        enEspritText.SetText("EN ESPRIT");
        isInvincible = true;
        timeBonusINV = 0;
        timeMaxBonusINV = timeBonus;
    }

    public void upSpeed(float multiplier, float timeBonus)
    {
        timeBonusMS = 0;
        if (bonusMSMultiplier == 1)
        {
            particleMS.SetActive(true);
            bonusMSText.SetText("MS+");
            bonusMSMultiplier = multiplier;
            updateMoveSpeed(1f);
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
        timeBonusFR = 0;
        if (bonusASMultiplier == 1)
        {
            particleAS.SetActive(true);
            bonusASText.SetText("AS+");
            bonusASMultiplier = multiplier;
            updateATKSpeed(1f);
        }
        timeMaxBonusFR = timeBonus;
    }

    void setSCOREText()
    {
        scoreText.SetText("MOULAGA : {0} $", moulaga);
    }

    // Start is called before the first frame update
    void Start()
    {
        myGun = gunList[indexActive];
        myGun.timeBetweenShots = myGun.baseTimeBetweenShots * attackSpeedMultiplier;
        myGun.reloadingTime = myGun.reloadingTime * reloadSpeedMultiplier;
        myGun.gameObject.SetActive(true);

        weaponText.SetText(myGun.weaponName);
        enEspritText.SetText("");
        bonusMSText.SetText("");
        bonusASText.SetText("");

        moulaga = 0;
        kills = 0;

        setSCOREText();
        myRigidBody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
    }

    public void updateATKSpeed(float multiplier)
    {
        attackSpeedMultiplier = multiplier * attackSpeedMultiplier;
        myGun.timeBetweenShots = myGun.baseTimeBetweenShots * attackSpeedMultiplier / bonusASMultiplier;
    }

    public void updateMoveSpeed(float multiplier)
    {
        moveSpeedMultiplier = multiplier * moveSpeedMultiplier;
        moveSpeed = baseMoveSpeed * moveSpeedMultiplier * bonusMSMultiplier;
    }

    public void updateReloadSpeed(float multiplier)
    {
        reloadSpeedMultiplier = multiplier * reloadSpeedMultiplier;
        myGun.reloadingTime = myGun.baseReloadingTime * reloadSpeedMultiplier;
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

    void checkBonusINV()
    {
        if (timeBonusINV < timeMaxBonusINV) timeBonusINV += Time.deltaTime;
        if (timeBonusINV > 0)
        {
            enEspritText.transform.position += 50 * timeBonusINV * new Vector3(1, 0, 0);
        }
        if (timeBonusINV > timeMaxBonusINV)
        {
            enEspritText.transform.position = new Vector3(-100, 0, 0);
            enEspritText.SetText("");
            particleINV.SetActive(false);
            isInvincible = false;
            timeBonusINV = 0;
        }
    }
    
    void checkBonusFR()
    {
        if (timeBonusFR < timeMaxBonusFR) timeBonusFR += Time.deltaTime;
        if (timeBonusFR > timeMaxBonusFR)
        {
            particleAS.SetActive(false);
            bonusASText.SetText("");
            timeBonusFR = 0;
            bonusASMultiplier = 1f;
            myGun.timeBetweenShots = myGun.baseTimeBetweenShots * attackSpeedMultiplier / bonusASMultiplier;
        }
    }

    void checkBonusMS()
    {
        if (timeBonusMS < timeMaxBonusMS) timeBonusMS += Time.deltaTime;
        if (timeBonusMS > timeMaxBonusMS)
        {
            particleMS.SetActive(false);
            bonusMSText.SetText("");
            timeBonusMS = 0;
            bonusMSMultiplier = 1f;
            moveSpeed = baseMoveSpeed * moveSpeedMultiplier * bonusMSMultiplier;
        }
    }

    void checkBonusHP()
    {
        if (timeBonusHP < timeMaxBonusHP) timeBonusHP += Time.deltaTime;
        if (timeBonusHP > timeMaxBonusHP)
        {
            particleHP.SetActive(false);
            timeBonusHP = 0;
            timeMaxBonusHP = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        setSCOREText();

        checkBonusFR();
        checkBonusHP();
        checkBonusMS();
        checkBonusINV();

        if(alive)
        {
            if (myGun.actualReloading < myGun.reloadingTime) magazineText.SetText("RELOADING");
            else magazineText.SetText(myGun.nbBallesInChargeur + "/" + myGun.nbBallesTot);

            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * moveSpeed * moveSpeedMultiplier;

            // Rotation du personnage

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            // Switch d'Arme
            if(Input.mouseScrollDelta.y != 0)
            {
                
                bool actualState = myGun.isFiring;

                myGun.timeBetweenShots = myGun.baseTimeBetweenShots * attackSpeedMultiplier;
                myGun.reloadingTime = myGun.baseReloadingTime * reloadSpeedMultiplier;
                myGun.isFiring = false;

                gunList[indexActive].gameObject.SetActive(false);

                indexActive = (indexActive + (int)Input.mouseScrollDelta.y) % (int)gunList.Count;
                if(indexActive < 0) indexActive = (int)gunList.Count - 1;

                gunList[indexActive].gameObject.SetActive(true);

                myGun = gunList[indexActive];

                myGun.timeBetweenShots = myGun.baseTimeBetweenShots * attackSpeedMultiplier / bonusASMultiplier;
                myGun.reloadingTime = myGun.baseReloadingTime * reloadSpeedMultiplier;

                myGun.isFiring = actualState;

                weaponText.SetText(myGun.weaponName);
                magazineText.SetText(myGun.nbBallesInChargeur + "/" + myGun.nbBallesTot);
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

            if (Input.GetKey(KeyCode.P))
            {
                moulaga += 1000;
            }

            if (Input.GetMouseButtonDown(0))
            {
                myGun.isFiring = true;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                myGun.setReload();
            }
        }
        else
        {
            myRigidBody.velocity = new Vector3(0, 0, 0); 
        }
    }
}