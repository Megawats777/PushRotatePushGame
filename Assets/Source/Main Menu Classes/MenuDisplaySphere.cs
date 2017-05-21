using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplaySphere : MonoBehaviour
{
    // A list of possible player skin materials
    [SerializeField]
    private Material[] playerSkinMaterials;

    // Component references
    private Renderer meshRenderer;


    // Called before start
    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Set the player skin of this menu display sphere
    public void setPlayerSkin(int selectionNum)
    {
        meshRenderer.material = playerSkinMaterials[selectionNum];

        // Set the selected player skin
        PersistentDataHolder.setSelectedPlayerSkin(playerSkinMaterials[selectionNum]);
    }
}
