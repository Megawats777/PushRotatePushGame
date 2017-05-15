using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // The score value of the target
    [SerializeField]
    private int scoreValue = 10;

    // Reference to the popup text prefab
    [SerializeField]
    protected PopUpText popUpTextObject;

    // External References
    protected Player playerRef;

    // Called before start
    protected void Awake()
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
            // Destroy this target
            destroyTarget();
        }
    }

    // Destroy this target
    public virtual void destroyTarget()
    {
        // Add to the player's score
        playerRef.increasePlayerScore(scoreValue);

        // Spawn the popup text object
        // Set the content of the popup text to be the score value of this target
        Vector3 popUpTextSpawnPosition = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
        PopUpText spawnedPopUpText = Instantiate(popUpTextObject, popUpTextSpawnPosition, Quaternion.identity);
        spawnedPopUpText.setPopUpTextContent("+" + scoreValue);

        

        gameObject.SetActive(false);
    }

}
