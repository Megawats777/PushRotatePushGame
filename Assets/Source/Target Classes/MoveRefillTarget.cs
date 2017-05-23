using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRefillTarget : Target
{
    // The amount of moves this target will refill to the player
    [SerializeField]
    private int moveRefillAmount = 1;

    // Called before start
    private void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    // Destroy this target
    public override void destroyTarget()
    {
        // Add to the moves of the player
        playerRef.setMovesLeft(playerRef.getMovesLeft() + moveRefillAmount);

        if (!popUpTextObject)
            print("none");

        // Spawn a pop up text object
        Vector3 popUpTextSpawnLocation = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
        PopUpText spawnedPopUpText = Instantiate(popUpTextObject, popUpTextSpawnLocation, Quaternion.identity);
        popUpTextObject.setPopUpTextContent("+" + moveRefillAmount + " moves added!");

        base.destroyTarget();
    }
}
