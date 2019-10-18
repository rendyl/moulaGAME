using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    public float baseMoveSpeed = 7;

    float timeBonusMS = 0;
    float timeMaxBonusMS = 0;

    public bool isInvincible = false;
    float timeBonusINV = 0;
    float timeMaxBonusINV = 0;

    float timeBonusFR = 0;
    float timeMaxBonusFR = 0;

    private Rigidbody myRigidBody;
    public int kills;
    public int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusMSText;
    public TextMeshProUGUI bonusASText;

    public GameObject particleINV;
    public GameObject particleMS;
    public GameObject particleAS;

    public bool alive = true;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
	
	private Camera mainCamera;
    public GunController myGun;

    public void invincible(float timeBonus)
    {
        particleINV.SetActive(true);
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

    void setScore()
    {
        score = kills * 50;
    }

    void setSCOREText()
    {
        scoreText.SetText("MOULAGA : {0} $", score);
    }

    // Start is called before the first frame update
    void Start()
    {
        bonusMSText.SetText("");
        bonusASText.SetText("");
        kills = 0;
        setSCOREText();
        myRigidBody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
		myRigidBody.velocity = moveVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        setScore();
        setSCOREText();

        if(alive)
        {
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
            if (timeBonusINV > timeMaxBonusINV)
            {
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

            if (Input.GetMouseButtonDown(0))
            {
                myGun.isFiring = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                myGun.isFiring = false;
            }
        }
    }
}
