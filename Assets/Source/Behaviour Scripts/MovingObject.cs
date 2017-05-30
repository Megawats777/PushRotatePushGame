using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    // The speed multiplier of the object
    [SerializeField]
    private float speedMultiplier = 1.0f;

    // The speed curve of the object
    [SerializeField]
    private AnimationCurve speedCurve;

    // The current time of the speed curve
    [SerializeField]
    private float currentSpeedCurveTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // The used movement speed
        // Set the used movement speed to be the current position in the curve
        float usedMovementSpeed = speedCurve.Evaluate(currentSpeedCurveTime);

        // Increase the current time of the speed curve
        currentSpeedCurveTime += Time.deltaTime * speedMultiplier;
        currentSpeedCurveTime = Mathf.Clamp(currentSpeedCurveTime, 0.0f, speedCurve.keys[speedCurve.length - 1].time);

        // Add to the object's position based on the used movement speed
        transform.Translate(transform.up * usedMovementSpeed * Time.deltaTime, Space.World);
	}
}
