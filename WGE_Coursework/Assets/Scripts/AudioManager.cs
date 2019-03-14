using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // Variables 
    public AudioClip destroyBlockSound;
    public AudioClip placeBlockSound;
    




    // Play the Destroy Block Audio
    void PlayBlockSound(int blockType)
    {
        if (blockType == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
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
