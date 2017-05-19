using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    // The scene this stage is attached to
    public string attatchedScene = string.Empty;

    // HUD Objects
    [SerializeField]
    private Text highScoreText;
    private Button button;

	// Use this for initialization
	void Start ()
    {
        // Set the content of the high score text object
        highScoreText.text = SaveGameManager.loadLevelHighScore(attatchedScene).ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
