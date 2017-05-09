using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    // The time it takes for the camera to reach the player's position
    [SerializeField]
    private float timeToReachPlayerPosition = 3.0f;

    Vector3 velocity = Vector3.zero;

    // External References
    Player player;


    // Called before start
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Called before physics calculations
    private void FixedUpdate()
    {

        Vector3 cameraFollowPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 20);
        // Always blend to the X and Z position of the player
        //transform.position = Vector3.SmoothDamp(transform.position, cameraFollowPosition, ref velocity, Time.deltaTime * timeToReachPlayerPosition);
    }
}
