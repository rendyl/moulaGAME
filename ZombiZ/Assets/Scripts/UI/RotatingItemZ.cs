using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItemZ : MonoBehaviour
{

	public float rotationMaxSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, 45 * rotationMaxSpeed * Time.deltaTime);
    }
}
