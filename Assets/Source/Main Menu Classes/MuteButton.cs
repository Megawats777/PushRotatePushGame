using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    // Reference to the text component of the button
    [SerializeField]
    private Text buttonText;


	// Use this for initialization
	void Start ()
    {
        // Set button text
        setButtonText();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Set button text
    public void setButtonText()
    {
        // If audio is enabled
        // Set the button text accordingly
        if (SaveGameManager.getIsAudioEnabled() == true)
        {
            buttonText.text = "Mute Audio";
        }

        // If audio is not enabled
        // Set the button text accordingly
        else if (SaveGameManager.getIsAudioEnabled() == false)
        {
            buttonText.text = "Unmute Audio";
        }
    }
}
