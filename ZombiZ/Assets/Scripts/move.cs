using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Z)) movement.z += 1;
        if (Input.GetKey(KeyCode.Q)) movement.x -= 1;
        if (Input.GetKey(KeyCode.S)) movement.z -= 1;
        if (Input.GetKey(KeyCode.D)) movement.x += 1;
        transform.position += (movement.normalized * speed);
    }
}
