using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHUDRoot : MonoBehaviour
{
    // Reference to the flash image game object
    [SerializeField]
    private GameObject flashImageGameObject;

	// Use this for initialization
	void Start ()
    {
        // Enabled the flash image game object
        flashImageGameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
