using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CinematicEffects;

public class GameCamera : MonoBehaviour
{
    // Post process effects component references
    private DepthOfField dofComponent;
    private Bloom bloomComponent;
    private AntiAliasing antiAliasingComponent;
    private AmbientOcclusion ambientOcclusionComponent;

    // External references
    private GameManager gameManager;

    // Called before start
    private void Awake()
    {
        dofComponent = GetComponent<DepthOfField>();
        bloomComponent = GetComponent<Bloom>();
        antiAliasingComponent = GetComponent<AntiAliasing>();
        ambientOcclusionComponent = GetComponent<AmbientOcclusion>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start ()
    {
        // Configure graphics settings
        configureGraphicsSettings();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the game manager exists
        if (gameManager)
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
    }

    // Called before physics calculations
    private void FixedUpdate()
    {

    }

    // Configure graphics settings
    private void configureGraphicsSettings()
    {
        // If the player is on the low setting
        if (QualitySettings.GetQualityLevel() == 0)
        {
            bloomComponent.settings.highQuality = false;
            ambientOcclusionComponent.enabled = false;
            antiAliasingComponent.enabled = false;
            dofComponent.settings.filteringQuality = DepthOfField.QualityPreset.Low;
        }

        // If the player is on the medium setting
        else if (QualitySettings.GetQualityLevel() == 1)
        {
            bloomComponent.settings.highQuality = false;
            ambientOcclusionComponent.enabled = true;
            antiAliasingComponent.enabled = true;
            antiAliasingComponent.method = 1; // FXAA is used
            dofComponent.settings.filteringQuality = DepthOfField.QualityPreset.Medium;
        }

        // If the player is on the high setting
        else if (QualitySettings.GetQualityLevel() == 2)
        {
            bloomComponent.settings.highQuality = true;
            ambientOcclusionComponent.enabled = true;
            antiAliasingComponent.enabled = true;
            antiAliasingComponent.method = 1; // FXAA is used
            dofComponent.settings.filteringQuality = DepthOfField.QualityPreset.Medium;
        }

        // If the player is on the ultra setting
        else if (QualitySettings.GetQualityLevel() == 3)
        {
            bloomComponent.settings.highQuality = true;
            ambientOcclusionComponent.enabled = true;
            antiAliasingComponent.enabled = true;
            antiAliasingComponent.method = 0; // SMAA is used
            dofComponent.settings.filteringQuality = DepthOfField.QualityPreset.High;
        }
    }
}
