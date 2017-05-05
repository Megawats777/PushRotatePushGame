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

    // The respawn location of the player
    [HideInInspector]
    public Vector3 respawnLocation;

    // Is input enabled
    private bool isInputEnabled = true;

    // Component references
    private Rigidbody rb;
    private Renderer meshRenderer;

    // Called before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start ()
    {
        respawnLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {  
        // If input is enabled
        if (isInputEnabled == true)
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
    }

    // Called before physics calculations
    private void FixedUpdate()
    {
        // Control player movement state
        controlPlayerMovementState();
    }

    // When this object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // If the colliding object is a wall
        if (collision.transform.CompareTag("Wall"))
        {
            // Respawn the player
            StartCoroutine(respawnPlayer());
        }
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
            rb.isKinematic = true;
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

    // Disable the player
    public void disablePlayer()
    {
        // Disable the mesh renderer
        // Start rotating the player
        // Disable player input
        // Disable any child objects
        meshRenderer.enabled = false;
        isInputEnabled = false;
        isRotating = true;
    }

    // Enable the player
    public void enablePlayer()
    {
        // Enable the mesh renderer
        // Start rotating the player
        // Enable player input
        // Enable any child objects
        meshRenderer.enabled = true;
        isInputEnabled = true;
        isRotating = true;
    }

    // Respawn the player
    public IEnumerator respawnPlayer()
    {
        // Disable the player
        disablePlayer();

        // Have a delay
        yield return new WaitForSeconds(1.0f);

        // Set the location of the player to be the respawn location
        transform.position = respawnLocation;

        // Enable the player
        enablePlayer();
    }
}


               
                

