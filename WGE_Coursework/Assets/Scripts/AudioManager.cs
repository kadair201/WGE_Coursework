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




    // Play the block audio for each block type
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
                audioSource.PlayOneShot(placeDirtBlockSound);
                break;
            case 3:
                audioSource.PlayOneShot(placeSandBlockSound);
                break;
            case 4:
                audioSource.PlayOneShot(placeStoneBlockSound);
                break;
            default:
                audioSource.PlayOneShot(destroyBlockSound);
                break;
        }
    }




    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockChanged += PlayBlockSound;
    }




    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockChanged -= PlayBlockSound;
    }
}
