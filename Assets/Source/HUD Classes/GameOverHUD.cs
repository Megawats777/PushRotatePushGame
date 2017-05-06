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

    // External references
    private Player playerRef;

    // Called before start
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        hudContentRoot.SetActive(false);
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
        playerScoreText.text = "Your Score: " + playerRef.getScore();
    }
}
