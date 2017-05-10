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

    // External references
    private Player playerRef;

    // Called before start
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        hudContentRoot.SetActive(true);
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
        scoreText.text = "Score: " + playerRef.getScore();
        movesLeftText.text = "Moves Left: " + playerRef.getMovesLeft();
        highScoreText.text = "High Score: " + playerRef.getHighScore();
    }
}
