﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingObject : MonoBehaviour
{
    // The max scale of the object
    [SerializeField]
    private Vector3 maxObjectScale = Vector3.one;

    // The min scale of the object
    [SerializeField]
    private Vector3 minObjectScale = Vector3.zero;

    // The scale lerp value
    [SerializeField]
    private float scaleLerpValue = 0.5f;

    // A scale speed multiplier
    [SerializeField]
    private float scaleSpeedMultiplier = 2.0f;

    // Scale animation curve
    [Tooltip("Keep time values of all keys between 0 and 1. \nOr else everything will break!"),SerializeField]
    private AnimationCurve scaleCurve;

    // The current time in the animation curve
    [SerializeField]
    private float curveTime = 0.0f;

    // Is the scale animation looping
    [SerializeField]
    private bool isLooping = true;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Set the scaler lerp value based on the scale curve
        // Get the value of the scale curve based on the curveTime variable
        scaleLerpValue = scaleCurve.Evaluate(curveTime);
        curveTime += Time.deltaTime * scaleSpeedMultiplier;
        curveTime = Mathf.Clamp(curveTime, 0.0f, 1.0f);

        // If the scaling is looping
        // Repeat the curve time value
        if (isLooping == true)
        {
            curveTime = Mathf.Repeat(curveTime, 1.0f);
        }
        
        // Blend between the two object scales based on the scale lerp value
        transform.localScale = Vector3.Lerp(minObjectScale, maxObjectScale, scaleLerpValue);
	}
}
