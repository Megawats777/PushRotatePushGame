using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The current game state
    public PossibleGameStates currentGameStates = PossibleGameStates.Active;

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
        print("Game Finished");
        currentGameStates = PossibleGameStates.Finished;
        mainHUDRef.hideHUD();
        gameOverHUDRef.showHUD();
    }

    // Pause the game
    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        print("Game Paused");
        currentGameStates = PossibleGameStates.Paused;
        mainHUDRef.hideHUD();
        pauseHUDRef.showHUD();
    }

    // Resume the game
    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        print("Game Resumed");
        currentGameStates = PossibleGameStates.Active;
        pauseHUDRef.hideHUD();
        mainHUDRef.showHUD();
    }
}
