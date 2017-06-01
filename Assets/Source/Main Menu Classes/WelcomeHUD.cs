using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeHUD : GameHUDBase
{
    // Reference to the game title gameObject
    [SerializeField]
    private GameObject gameTitleObject;


    public override void hideHUD()
    {
        hudContentRoot.SetActive(false);
        gameTitleObject.SetActive(false);
    }

    public override void showHUD()
    {
        hudContentRoot.SetActive(true);
        gameTitleObject.SetActive(true);
    }

    public override void updateHUDContent()
    {
        hudContentRoot.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        hudContentRoot.SetActive(true);
        gameTitleObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
