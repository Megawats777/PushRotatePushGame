using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirections
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

    // The current rotation direction
    private RotationDirections currentRotationDirection = RotationDirections.Clockwise;

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
            }

            // If the player is rotating
            // Set the player to not be rotating
            else
            {
                isRotating = false;
            }
        }

        // If the W key is pressed
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // If the player is rotating
            // Flip the current rotation direction
            if (isRotating == true)
            {
                flipRotationDirection();
            }
        }
    }

    // Called before physics calculations
    private void FixedUpdate()
    {
        // Control player movement state
        controlPlayerMovementState();
    }

    // Control player movement state
    private void controlPlayerMovementState()
    {
        // If the player is not rotating
        if (isRotating == false)
        {
            // Move the player forward
            rb.isKinematic = false;
            rb.velocity = transform.up * forwardMovementSpeed;
        }

        // If the player is rotating
        else
        {
            rb.velocity = Vector3.zero;
            float usedRotationSpeed = 0.0f;

            // If the shift key is pressed
            // Use the boost rotation speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                usedRotationSpeed = boostRotationSpeed;
            }

            // If the shift key is not pressed
            // Use the regular rotation speed
            else
            {
                usedRotationSpeed = rotationSpeed;
            }

            // If the current rotation direction is clockwise
            if (currentRotationDirection == RotationDirections.Clockwise)
            {
                // Rotate the player clockwise
                transform.Rotate(transform.forward * -usedRotationSpeed * Time.deltaTime);
            }

            // If the current rotation direction is counterclockwise
            else if (currentRotationDirection == RotationDirections.CounterClockwise)
            {
                // Rotate the player counterclockwise
                transform.Rotate(transform.forward * usedRotationSpeed * Time.deltaTime);
            }
        }
    }

    // Flip rotation direction
    private void flipRotationDirection()
    {
        // If the current rotation direction is clockwise
        // Set the current rotation direction to be counterclockwise
        if (currentRotationDirection == RotationDirections.Clockwise)
        {
            currentRotationDirection = RotationDirections.CounterClockwise;
            print("Current rotation direction: " + currentRotationDirection.ToString());
        }
            

        // If the current rotation direction is counterclockwise
        // Set the current rotation direction to be clockwise
        else if (currentRotationDirection == RotationDirections.CounterClockwise)
        {
            currentRotationDirection = RotationDirections.Clockwise;
            print("Current rotation direction: " + currentRotationDirection.ToString());
        }
            
    }
}


               
                

