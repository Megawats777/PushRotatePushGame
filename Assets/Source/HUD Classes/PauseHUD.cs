using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHUD : GameHUDBase
{

    // External references
    private GameManager gameManager;

    // Called before start
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        hideHUD();
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

    }

    // Resume game wrapper method
    public void callGameManagerResumeGameMethod()
    {
        gameManager.resumeGame();
    }
}
