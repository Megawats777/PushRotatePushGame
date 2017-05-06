using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // External references
    private MainHUD mainHUDRef;
    private GameOverHUD gameOverHUDRef;
    private PauseHUD pauseHUDRef;

    // Called before start
    private void Awake()
    {
        mainHUDRef = FindObjectOfType<MainHUD>();
        gameOverHUDRef = FindObjectOfType<GameOverHUD>();
        pauseHUDRef = FindObjectOfType<PauseHUD>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Start the game
    public void startGame()
    {

    }

    // End the game
    public void endGame()
    {
        mainHUDRef.hideHUD();
        gameOverHUDRef.showHUD();
    }

    // Pause the game
    public void pauseGame()
    {

    }
}
