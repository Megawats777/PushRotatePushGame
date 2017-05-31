using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GlobalButtonActions : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Load Current Level
    public void loadCurrentLevel()
    {
        StartCoroutine(loadLevelProcess(SceneManager.GetActiveScene().name));
    }

    // Load Level
    public void loadLevel(string levelName)
    {
        StartCoroutine(loadLevelProcess(levelName));
    }

    // Load level process
    private IEnumerator loadLevelProcess(string name)
    {
        // Find the player in the scene
        Player playerRef = FindObjectOfType<Player>();

        // If the player exists
        // Disable the player's input
        if (playerRef)
        {
            playerRef.isInputEnabled = false;
        }

        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadSceneAsync(name);

        // Set the time scale to 1
        Time.timeScale = 1.0f;
    }


    // Quit Application
    public void quitApplication()
    {
        Application.Quit();
    }
}
