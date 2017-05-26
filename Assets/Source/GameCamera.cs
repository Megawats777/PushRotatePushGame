using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CinematicEffects;

public class GameCamera : MonoBehaviour
{
    // Post process effects component references
    private DepthOfField dofComponent;

    // External references
    private GameManager gameManager;

    // Called before start
    private void Awake()
    {
        dofComponent = GetComponent<DepthOfField>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		// While the game is paused or finished
        // Set the depth of field blur radius to be 35
        if (gameManager.currentGameState == PossibleGameStates.Paused || gameManager.currentGameState == PossibleGameStates.Finished)
        {
            dofComponent.focus.farBlurRadius = 35.0f;
        }

        // While the game is active
        // Set depth of field blur radius to be 0
        else if (gameManager.currentGameState == PossibleGameStates.Active)
        {
            dofComponent.focus.farBlurRadius = 0.0f;
        }
    }

    // Called before physics calculations
    private void FixedUpdate()
    {

    }
}
