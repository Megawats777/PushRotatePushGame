using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // A list of songs to play
    [SerializeField]
    private AudioClip[] songList;

    // Reference to the audio source component
    private AudioSource audioSource;

    // Called before start
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    // Use this for initialization
    void Start ()
    {
        // Play a song
        playSong();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Play a song
    private void playSong()
    {
        // Select a song to play
        AudioClip chosenSong = songList[Random.Range(0, songList.Length - 1)];

        // Play the song
        audioSource.clip = chosenSong;
        audioSource.Play();
    }

}
