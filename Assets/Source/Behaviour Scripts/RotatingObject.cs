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

    // Does the object start out rotating
    [SerializeField]
    private bool rotatesOnStart = true;

    // Is the object rotating
    public bool isRotating = true;


    // Use this for initialization
    void Start ()
    {
        // Determine if the object rotates on start
        if (rotatesOnStart)
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the object is rotating
        if (isRotating)
        { 
            // Rotate the object based on the given values
            transform.Rotate(new Vector3(rotationSpeedXAxis, rotationSpeedYAxis, rotationSpeedZAxis) * Time.deltaTime);
        }
    }

    // Start rotating
    public void startRotating()
    {
        isRotating = true;
    }

    // Stop rotating
    public void stopRotating()
    {
        isRotating = false;
    }

    // Set object rotation speed
    public void setObjectRotationSpeed(float speed, Axis chosenAxis)
    {
        // Depending on the chosen axis set the new rotation speed on the appropriate axis
        if (chosenAxis == Axis.X)
        {
            rotationSpeedXAxis = speed;
        }
        else if (chosenAxis == Axis.Y)
        {
            rotationSpeedYAxis = speed;
        }
        else if (chosenAxis == Axis.Z)
        {
            rotationSpeedZAxis = speed;
        }
    }
}
