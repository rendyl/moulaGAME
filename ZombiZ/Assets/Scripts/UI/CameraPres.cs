using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPres : MonoBehaviour
{
    public Transform player;
    public float turnSpeed = 4.0f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = - player.position + transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Time.deltaTime * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
