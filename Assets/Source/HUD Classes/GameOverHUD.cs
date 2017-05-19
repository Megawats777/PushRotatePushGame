using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHUD : GameHUDBase
{
    // HUD Objects
    [Header("HUD Objects")]
    [SerializeField]
    private Text playerScoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text newHighScoreMessageText;

    // External references
    private Player playerRef;

    // Called before start
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        hudContentRoot.SetActive(false);
        newHighScoreMessageText.enabled = false;
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
        playerScoreText.text = playerRef.getScore().ToString();
        highScoreText.text = playerRef.getHighScore().ToString();
    }

    // Show the new high score message
    public void showNewHighScoreMessage()
    {
        newHighScoreMessageText.enabled = true;
    }
}
