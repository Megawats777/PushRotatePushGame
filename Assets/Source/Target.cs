using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

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
            gameObject.SetActive(false);
        }

    }

}
