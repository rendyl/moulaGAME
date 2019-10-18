using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 0.1f;
    public float diviseur = 10;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;   
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, target.position + offset + new Vector3(Input.mousePosition.x / diviseur, 0, Input.mousePosition.y / diviseur), speed * Time.deltaTime);
    }
}
