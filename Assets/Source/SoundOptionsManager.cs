using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptionsManager : MonoBehaviour
{
    // Called before start
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        // Set audio volume scale
        setAudioVolumeScale();
    }

    // Toggle audio
    public void toggleAudio()
    {
        // If audio is enabled
        // Disable audio
        if (SaveGameManager.getIsAudioEnabled() == true)
        {
            SaveGameManager.saveIfAudioIsEnabled(false);
        }

        // Otherwise if audio is not enabled
        // Enabled audio
        else
        {
            SaveGameManager.saveIfAudioIsEnabled(true);
        }

        // Set the audio volume scale
        setAudioVolumeScale();
    }

    // Set audio volume scale
    public void setAudioVolumeScale()
    {
        // If sound is enabled
        // Set the audio listener volume to be 1
        if (SaveGameManager.getIsAudioEnabled() == true)
        {
            AudioListener.volume = 1.0f;
        }

        // Otherwise
        // Set the audio listener colume to be 0
        else
        {
            AudioListener.volume = 0.0f;
        }
    }
}
