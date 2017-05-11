using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // The rotation speed in three axis values
    [SerializeField]
    private float rotationSpeedXAxis = 20.0f;
    [SerializeField]
    private float rotationSpeedYAxis = 20.0f;
    [SerializeField]
    private float rotationSpeedZAxis = 20.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Rotate the object based on the given values
        transform.Rotate(new Vector3(rotationSpeedXAxis, rotationSpeedYAxis, rotationSpeedZAxis) * Time.deltaTime);	
	}
}
