using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationOrientations
{
    Clockwise,
    CounterClockwise
}


public class Player : MonoBehaviour
{
    // The rotation and forward movement speed
    [SerializeField, Header("Movement Properties")]
    private float rotationSpeed = 10.0f;
    [SerializeField]
    private float boostRotationSpeed = 20.0f;
    [SerializeField]
    private float forwardMovementSpeed = 10.0f;

    // The current rotation orientation
    private RotationOrientations currentRotationOrientation = RotationOrientations.Clockwise;

    // Is the player rotating
    [SerializeField]
    private bool isRotating = true;

    // Component references
    private Rigidbody rb;

    // Called before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
		// If the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the player is not rotating
            // Set the player to be rotating
            if (isRotating == false)
            {
                isRotating = true;

                /*
                // If the current rotation orientation is clockwise
                // Set the current rotation orientation to be counterclockwise
                if (currentRotationOrientation == RotationOrientations.Clockwise)
                    currentRotationOrientation = RotationOrientations.CounterClockwise;

                // If the current rotation orientation is counterclockwise
                // Set the current rotation orientation to be clockwise
                else if (currentRotationOrientation == RotationOrientations.CounterClockwise)
                    currentRotationOrientation = RotationOrientations.Clockwise;
                    */
            }

            // If the player is rotating
            // Set the player to not be rotating
            else
            {
                isRotating = false;
            }
        }
    }

    // Called before physics calculations
    private void FixedUpdate()
    {
        // If the player is not rotating
        if (isRotating == false)
        {
            // Move the player forward
            rb.isKinematic = false;
            rb.AddForce(transform.up * forwardMovementSpeed);
        }

        // If the player is rotating
        else
        {
            rb.isKinematic = true;
            float usedRotationSpeed = 0.0f;

            // If the shift key is pressed
            // Use the boost rotation speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                usedRotationSpeed = boostRotationSpeed;
                print("Is Boosting");
            }

            // If the shift key is not pressed
            // Use the regular rotation speed
            else
            {
                usedRotationSpeed = rotationSpeed;
                print("Is Not Boosting");
            }

            // If the current rotation orientation is clockwise
            if (currentRotationOrientation == RotationOrientations.Clockwise)
            {
                // Rotate the player clockwise
                transform.Rotate(transform.forward * -usedRotationSpeed * Time.deltaTime);
            }

            // If the current rotation orientation is counterclockwise
            else if (currentRotationOrientation == RotationOrientations.CounterClockwise)
            {
                // Rotate the player counterclockwise
                transform.Rotate(transform.forward * usedRotationSpeed * Time.deltaTime);
            }
        }
    }
}


               
                

