using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    // The time it takes for the camera to reach the player's position
    [SerializeField]
    private float timeToReachPlayerPosition = 3.0f;

    // The distance from the player
    private float distanceFromPlayer;

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
        // Set the distance from the player
        distanceFromPlayer = transform.position.z - player.transform.position.z;
        print(distanceFromPlayer);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Called before physics calculations
    private void FixedUpdate()
    {

    }

    // Called after physics calculations
    private void LateUpdate()
    {
        // Blend to the player's position
        //Vector3 followPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + distanceFromPlayer);
        //transform.position = Vector3.SmoothDamp(transform.position, followPosition, ref velocity, Time.deltaTime * timeToReachPlayerPosition);

        // Blend to the player's rotation
    }


}
