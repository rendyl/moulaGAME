using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
	public float rotationMaxSpeed;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 45 * rotationMaxSpeed * Time.deltaTime);
    }
}
