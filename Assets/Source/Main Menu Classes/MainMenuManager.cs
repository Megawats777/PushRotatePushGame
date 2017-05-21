using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // The level to launch
    private string levelToLaunch = string.Empty;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Launch the game
    public void launchGame()
    {
        SceneManager.LoadSceneAsync(levelToLaunch);
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
