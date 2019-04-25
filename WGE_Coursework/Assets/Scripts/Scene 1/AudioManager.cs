using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // Variables 
    public AudioClip destroyBlockSound;
    public AudioClip placeGrassBlockSound;
    public AudioClip placeDirtBlockSound;
    public AudioClip placeSandBlockSound;
    public AudioClip placeStoneBlockSound;
    public AudioSource audioSource;




    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }




    // play block audio for each block type
    void PlayBlockSound(int blockType)
    {
        switch (blockType)
        {
            case 0:
                // play the destroy block sound effect
                audioSource.PlayOneShot(destroyBlockSound);
                break;
            case 1:
                // play the grass placement sound effect
                audioSource.PlayOneShot(placeGrassBlockSound);
                break;
            case 2:
                // play the dirt placement sound effect
                audioSource.PlayOneShot(placeDirtBlockSound);
                break;
            case 3:
                // play the sand placement sound effect
                audioSource.PlayOneShot(placeSandBlockSound);
                break;
            case 4:
                // play the stone placement sound effect
                audioSource.PlayOneShot(placeStoneBlockSound);
                break;
            default:
                audioSource.PlayOneShot(destroyBlockSound);
                break;
        }
    }




    // attach the method to the event when the gameObject is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockChanged += PlayBlockSound;
    }




    // detach the method from the event when the gameObject is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockChanged -= PlayBlockSound;
    }
}
