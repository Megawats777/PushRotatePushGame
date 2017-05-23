using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHUD : GameHUDBase
{

    public override void hideHUD()
    {
        hudContentRoot.SetActive(false);

    }

    public override void showHUD()
    {
        hudContentRoot.SetActive(true);

    }

    public override void updateHUDContent()
    {

    }

    // Use this for initialization
    void Start()
    {
        hudContentRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
