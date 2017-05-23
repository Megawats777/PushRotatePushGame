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
    [HideInInspector]
    public RotationDirections currentRotationDirection = RotationDirections.Clockwise;

    // Is the player rotating
    [SerializeField]
    public bool isRotating = true;

    // The player's score
    [Space(2.0f), SerializeField]
    private int score = 0;

    // The player's high score
    private int highScore = 0;

    /*--Combo Properties--*/
    [SerializeField]
    private int comboSize = 0;

    // The number of moves left
    [SerializeField]
    private int movesLeft = 5;

    // The respawn location of the player
    [HideInInspector]
    public Vector3 respawnLocation;

    // Is input enabled
    private bool isInputEnabled = true;


    /*--Child Object References--*/
    // The display mesh
    [Header("Child Object References"),SerializeField]
    private GameObject displayMesh;
    [SerializeField]
    private RotatingObject displayMeshRotatingObjectBehaviour;
    [SerializeField]
    private Renderer displayMeshRenderer;
    [SerializeField]
    private Material defaultMaterial;

    // A popup text object to spawn
    [Header("Objects to Spawn"), SerializeField]
    private PopUpText popUpTextObject;

    // Movement audio clips
    [Header("Movement Audio Clips"), SerializeField]
    private AudioClip startMoveAudioClip;
    [SerializeField]
    private AudioClip endMoveAudioClip;

    // Component references
    private Rigidbody rb;
    private Renderer meshRenderer;
    private LineRenderer lineRendererComp;
    private AudioSource movementAudioSource;

    // External references
    private GameManager gameManager;
    private GameOverHUD gameOverHUD;

    // Called before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<Renderer>();
        lineRendererComp = GetComponent<LineRenderer>();
        movementAudioSource = GetComponent<AudioSource>();

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

        // Set the rotation speed of the display mesh
        displayMeshRotatingObjectBehaviour.setObjectRotationSpeed(forwardMovementSpeed, Axis.X);

        // Set the skin of the player
        if (PersistentDataHolder.getSelectedPlayerSkin() != null)
        {
            displayMeshRenderer.material = PersistentDataHolder.getSelectedPlayerSkin();
        }
        else
        {
            displayMeshRenderer.material = defaultMaterial;
        }

        rb.velocity = Vector3.up * -2;
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
                    rb.velocity = Vector3.zero;
                    isRotating = true;

                    // Stop rotating the display mesh
                    displayMeshRotatingObjectBehaviour.stopRotating();

                    // End a move
                    endMove();
                }

                // If the player is rotating
                // Set the player to not be rotating
                else
                {
                    isRotating = false;

                    // Start rotating the display mesh
                    displayMeshRotatingObjectBehaviour.startRotating();

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
        // If the colliding object is a wall pr a boundary floor
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("BoundaryFloor"))
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
            //rb.isKinematic = false;
            //rb.velocity = transform.forward * forwardMovementSpeed;

            rb.AddForce(transform.forward * forwardMovementSpeed, ForceMode.VelocityChange);
        }

        // If the player is rotating
        else
        {
            //rb.isKinematic = true;
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
                transform.Rotate(transform.up * usedRotationSpeed * Time.deltaTime);
            }

            // If the current rotation direction is counterclockwise
            else if (currentRotationDirection == RotationDirections.CounterClockwise)
            {
                // Rotate the player counterclockwise
                transform.Rotate(transform.up * -usedRotationSpeed * Time.deltaTime);
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
        // Hide the display mesh
        // Start rotating the player
        // Hide the line renderer
        // Disable player input
        // Disable any child objects
        rb.isKinematic = true;
        displayMesh.SetActive(false);
        displayMeshRotatingObjectBehaviour.stopRotating();
        lineRendererComp.enabled = false;
        isInputEnabled = false;
        isRotating = true;
    }

    // Enable the player
    public void enablePlayer()
    {
        // Show the display mesh
        // Start rotating the player
        // Show the line renderer
        // Enable player input
        rb.isKinematic = false;
        displayMesh.SetActive(true);
        displayMeshRotatingObjectBehaviour.startRotating();
        lineRendererComp.enabled = true;
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

    // Increase combo size
    public void increaseComboSize()
    {
        setComboSize(getComboSize() + 1);
    }

    // End the combo
    public void endCombo()
    {
        // If the size of the combo is greater than 1
        if (getComboSize() > 1)
        {
            // The score from the combo
            int comboScore = getComboSize() * 10;

            // Add the combo score to the player's score
            increasePlayerScore(comboScore);

            print("Combo Size: " + comboSize);
            print("Combo Bonus: " + comboScore);

            // Spawn a pop up text object
            // Set the content of the pop up text
            Vector3 popUpTextSpawnLocation = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            PopUpText spawnedPopUpText = Instantiate(popUpTextObject, popUpTextSpawnLocation, Quaternion.identity);
            spawnedPopUpText.setPopUpTextContent("+" + comboScore + " Combo Bonus");

            // Reset the combo size to 0
            setComboSize(0);
        }
    }

    // Start a move
    public void startMove()
    {
        // Play a sound
        movementAudioSource.clip = startMoveAudioClip;
        movementAudioSource.Play();

        // Decrease the amount of moves left
        setMovesLeft(getMovesLeft() - 1);
    }

    // End a move
    public void endMove()
    {
        // Play a sound
        movementAudioSource.clip = endMoveAudioClip;
        movementAudioSource.Play();

        // End any existing combos
        endCombo();

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

    public int getComboSize()
    {
        return comboSize;
    }

    public void setComboSize(int comboSize)
    {
        this.comboSize = comboSize;
    }
}


               
                

