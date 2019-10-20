using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
	public float rotationMaxSpeed;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(transform.up + new Vector3(0, transform.position.y/2, 0), 45 * rotationMaxSpeed * Time.deltaTime);
    }
}
