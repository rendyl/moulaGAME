using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float baseSpeed;
    public float speed;
    public float diviseur;

    private float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        multiplier = (target.gameObject.GetComponent<PlayerController>().bonusMSMultiplier) * (target.gameObject.GetComponent<PlayerController>().moveSpeedMultiplier);
        offset = transform.position - target.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if ((target.gameObject.GetComponent<PlayerController>().bonusMSMultiplier * target.gameObject.GetComponent<PlayerController>().moveSpeedMultiplier) != multiplier)
        {
            multiplier = (target.gameObject.GetComponent<PlayerController>().bonusMSMultiplier * target.gameObject.GetComponent<PlayerController>().moveSpeedMultiplier);
            speed = baseSpeed * multiplier;
        }
        // transform.position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, target.position + offset + new Vector3(Mathf.Clamp(Input.mousePosition.x, -960f, 960f) / diviseur, 0, Input.mousePosition.y / diviseur), speed * Time.deltaTime);
    }
}
