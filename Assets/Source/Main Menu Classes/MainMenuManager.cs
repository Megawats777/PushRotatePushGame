using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Reference to the loading text gameobject
    [SerializeField]
    private GameObject loadingText;

    // Reference to the flash image gameobject
    [SerializeField]
    private GameObject flashImage;

    // Reference to the flash image animation controller
    [SerializeField]
    private Animator flashImageAnimController;

    // The level to launch
    private string levelToLaunch = string.Empty;

	// Use this for initialization
	void Start ()
    {
        flashImage.SetActive(true);
        loadingText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Launch the game
    public void launchGame()
    {
        showFlashImage();
        loadingText.SetActive(true);
        GetComponent<GlobalButtonActions>().loadLevel(levelToLaunch);
    }

    // Show the flash image
    private void showFlashImage()
    {
        flashImageAnimController.SetBool("isFadingOut", false);
    }

    // Hide the flash image
    private void hideFlashImage()
    {
        flashImageAnimController.SetBool("isFadingOut", true);
    }

    /*--Getters and Setters--*/

    public string getLevelToLaunch()
    {
        return levelToLaunch;
    }

    public void setLevelToLaunch(string levelToLaunch)
    {
        this.levelToLaunch = levelToLaunch;
    }
}
