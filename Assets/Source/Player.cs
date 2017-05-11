using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    // The player's score
    [Space(2.0f), SerializeField]
    private int score = 0;

    // The player's high score
    private int highScore = 0;

    // The number of moves left
    [SerializeField]
    private int movesLeft = 5;

    // The respawn location of the player
    [HideInInspector]
    public Vector3 respawnLocation;

    // Is input enabled
    private bool isInputEnabled = true;

    // Component references
    private Rigidbody rb;
    private Renderer meshRenderer;
    private LineRenderer lineRendererComp;

    // External references
    private GameManager gameManager;
    private GameOverHUD gameOverHUD;

    // Called before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<Renderer>();
        lineRendererComp = GetComponent<LineRenderer>();

        gameManager = FindObjectOfType<GameManager>();
        gameOverHUD = FindObjectOfType<GameOverHUD>();
    }

    // Use this for initialization
    void Start ()
    {
        score = 0;
        respawnLocation = transform.position;

        // Set the high score
        setHighScore(SaveGameManager.loadLevelHighScore(SceneManager.GetActiveScene().name));
        print(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_LevelScore.SAVE");
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

                    // End a move
                    endMove();
                }

                // If the player is rotating
                // Set the player to not be rotating
                else
                {
                    isRotating = false;

                    // Start a move
                    startMove();
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

            // If the esacape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // If the current game state is Active and is not Finished
                // Pause the game
                if (gameManager.currentGameStates == PossibleGameStates.Active && gameManager.currentGameStates != PossibleGameStates.Finished)
                {
                    gameManager.pauseGame();
                }

                // If the current game state is paused
                // Resume the game
                else if (gameManager.currentGameStates == PossibleGameStates.Paused)
                { 
                    gameManager.resumeGame();
                }
            }
        }
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            setHighScore(0);
            SaveGameManager.saveLevelHighScore(SceneManager.GetActiveScene().name, getHighScore());
        }

        

    }

    // Called before physics calculations
    private void FixedUpdate()
    {
        // Control player movement state
        controlPlayerMovementState();
    }

    // Called after physics calculations
    private void LateUpdate()
    {
        
    }

    // When this object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // If the colliding object is a wall
        if (collision.transform.CompareTag("Wall"))
        {
            // End a move
            endMove();

            // If the number of moves left is greater than 0
            if (getMovesLeft() > 0)
            {
                // Respawn the player
                StartCoroutine(respawnPlayer());
            }
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
            rb.velocity = transform.forward * forwardMovementSpeed;
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

            // If the S key is pressed
            // Set the rotation speed to be 0
            else if (Input.GetKey(KeyCode.S))
            {
                usedRotationSpeed = 0.0f;
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
                transform.Rotate(transform.up * -usedRotationSpeed * Time.deltaTime);
            }

            // If the current rotation direction is counterclockwise
            else if (currentRotationDirection == RotationDirections.CounterClockwise)
            {
                // Rotate the player counterclockwise
                transform.Rotate(transform.up * usedRotationSpeed * Time.deltaTime);
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
        // Hide the line renderer
        // Disable player input
        // Disable any child objects
        lineRendererComp.enabled = false;
        meshRenderer.enabled = false;
        isInputEnabled = false;
        isRotating = true;
    }

    // Enable the player
    public void enablePlayer()
    {
        // Enable the mesh renderer
        // Start rotating the player
        // Show the line renderer
        // Enable player input
        // Enable any child objects
        lineRendererComp.enabled = true;
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

    // Increase the player score
    public void increasePlayerScore(int scoreValue)
    {
        setScore(getScore() + scoreValue);
    }

    // Calculate high score
    public void calculateHighScore()
    {
        // If the player's score is greater than the high score
        if (didPlayerBeatHighScore())
        {
            // Set the high score to be the player's score
            // Save the high score
            setHighScore(getScore());
            SaveGameManager.saveLevelHighScore(SceneManager.GetActiveScene().name, getHighScore());
            gameOverHUD.showNewHighScoreMessage();
            print("High Score: " + getHighScore());
        }
    }

    // Did the player beat the high score
    public bool didPlayerBeatHighScore()
    {
        return getScore() > getHighScore();
    }

    // Start a move
    public void startMove()
    {
        // Decrease the amount of moves left
        setMovesLeft(getMovesLeft() - 1);
    }

    // End a move
    public void endMove()
    {
        // If the number of moves left equals 0
        // Disable the player
        // Calculate the high score
        // End the game
        if (getMovesLeft() <= 0)
        {
            disablePlayer();
            calculateHighScore();
            gameManager.endGame();
            print("Game Over");
        }
    }

    /*--Getters and Setters--*/

    public int getScore()
    {
        return score;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public int getMovesLeft()
    {
        return movesLeft;
    }

    public void setMovesLeft(int movesLeft)
    {
        this.movesLeft = movesLeft;
    }

    public int getHighScore()
    {
        return highScore;
    }

    public void setHighScore(int highScore)
    {
        this.highScore = highScore;
    }
}


               
                

