using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    // Reference to the text component
    [SerializeField]
    private Text textComp;

    // External References
    private GameCamera gameCamera;

    // Called before start
    private void Awake()
    {
        gameCamera = FindObjectOfType<GameCamera>();
    }

    // Use this for initialization
    void Start ()
    {
        // Destroy the object after 20 seconds
        Destroy(gameObject, 20.0f);

        if (gameCamera)
        {
            transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, 0.0f, 0.0f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Always look at the player
        //transform.LookAt(2 * transform.position - gameCamera.transform.position);
	}

    // Set the content of the pop up text component
    public void setPopUpTextContent(string content)
    {
        textComp.text = content;
    }
}
