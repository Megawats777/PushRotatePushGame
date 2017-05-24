using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // The score value of the target
    [SerializeField]
    private int scoreValue = 10;

    // Reference to the popup text prefab
    [SerializeField]
    protected PopUpText popUpTextObject;

    // The particle effect to spawn when this target is destroyed
    [SerializeField]
    protected GameObject destroyedParticleEffect;

    // The destroyed sound of the target
    [SerializeField]
    private AudioClip destroyedSound;

    // External References
    protected Player playerRef;

    // Component references
    private AudioSource targetAudioSource;
    private Renderer mainMeshRenderer;
    private Collider scoreTrigger;

    // Child object references
    [SerializeField]
    private GameObject childDisplayMesh;

    // Called before start
    protected void Awake()
    {
        targetAudioSource = GetComponent<AudioSource>();
        playerRef = FindObjectOfType<Player>();

        mainMeshRenderer = GetComponent<Renderer>();
        scoreTrigger = GetComponent<Collider>();
    }

    // Use this for initialization
    protected void Start ()
    {
        targetAudioSource.clip = destroyedSound;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // When an object overlaps with this target
    private void OnTriggerEnter(Collider other)
    {
        // If the overlaping object is the player
        if (other.CompareTag("Player"))
        {
            // Play a sound
            targetAudioSource.Play();

            // Destroy this target
            destroyTarget();
        }
    }

    /// // Destroy this target 
    public virtual void destroyTarget()
    {
        // Add to the player's score
        // Increase the player's combo
        playerRef.increasePlayerScore(scoreValue);
        playerRef.increaseComboSize();


        // Spawn the popup text object
        // Set the content of the popup text to be the score value of this target
        Vector3 popUpTextSpawnPosition = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
        PopUpText spawnedPopUpText = Instantiate(popUpTextObject, popUpTextSpawnPosition, Quaternion.identity);
        spawnedPopUpText.setPopUpTextContent("+" + scoreValue);

        // Spawn the destroyed particle effect
        GameObject spawnedParticleEffect = Instantiate(destroyedParticleEffect, transform.position, Quaternion.identity);
        //Destroy(spawnedParticleEffect, 20.0f);

        // Hide this object
        hideObject();
    }

    // Hide this object
    public void hideObject()
    {
        mainMeshRenderer.enabled = false;
        childDisplayMesh.SetActive(false);
        scoreTrigger.enabled = false;
    }

}
