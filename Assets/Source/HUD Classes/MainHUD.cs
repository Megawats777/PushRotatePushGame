using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : GameHUDBase
{
    // HUD objects
    [Header("HUD Objects")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text movesLeftText;
    [SerializeField]
    private GameObject clockwiseMovementImage;
    [SerializeField]
    private GameObject counterClockwiseMovementImage;
    [SerializeField]
    private Text moveNotifyText;
    [SerializeField]
    private GameObject highScoreBeatenNotifyText;
    [SerializeField]
    private Text moveControlText;



    // External references
    private Player playerRef;

    // Called before start
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
    }

    // Used for initialization
    private void Start()
    {
        
    }

    // Called every frame
    private void Update()
    {
        updateHUDContent();
    }


    public override void hideHUD()
    {
        hudContentRoot.SetActive(false);
    }

    public override void showHUD()
    {
        hudContentRoot.SetActive(true);
    }

    public override void updateHUDContent()
    {
        scoreText.text = playerRef.getScore().ToString();
        movesLeftText.text = playerRef.getMovesLeft().ToString();
        highScoreText.text = playerRef.getHighScore().ToString();

        // Depending on the player's current rotation direction
        // Show the appropriate rotation image
        if (playerRef.currentRotationDirection == RotationDirections.Clockwise)
        {
            clockwiseMovementImage.SetActive(true);
            counterClockwiseMovementImage.SetActive(false);
        }
        else
        {
            counterClockwiseMovementImage.SetActive(true);
            clockwiseMovementImage.SetActive(false);
        }

        // If the player only has one move left
        // Show the final move text object
        if (playerRef.getMovesLeft() == 1)
        {
            moveNotifyText.text = "Final Move";
            moveNotifyText.gameObject.SetActive(true);
        }
        else if (playerRef.getMovesLeft() == 0)
        {
            moveNotifyText.text = "No Moves Left";
            moveNotifyText.gameObject.SetActive(true);
        }
        else
        {
            moveNotifyText.gameObject.SetActive(false);
        }

        // If the player has beaten the high score
        // Show the high score notify object
        if (playerRef.getScore() > playerRef.getHighScore())
        {
            highScoreBeatenNotifyText.SetActive(true);
        }
        else if (playerRef.getScore() <= playerRef.getHighScore())
        {
            highScoreBeatenNotifyText.SetActive(false);
        }

        // Set the content of the move control text object
        // Based on if the player is rotating or not
        if (playerRef.isRotating)
        {
            moveControlText.text = "Spacebar: Move";
        }
        else
        {
            moveControlText.text = "Spacebar: Stop";
        }
    }
}
