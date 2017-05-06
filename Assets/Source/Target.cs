using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // External References
    private Player playerRef;

    // Called before start
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // When an object overlaps with this target
    private void OnTriggerEnter(Collider other)
    {
        // If the overlaping object is the player
        if (other.CompareTag("Player"))
        {
            // Add to the player's score
            // Destroy this target
            print("Target Destroyed");
            playerRef.increasePlayerScore(10);
            print("Player Score: " + playerRef.getScore());
            gameObject.SetActive(false);
        }

    }

}
