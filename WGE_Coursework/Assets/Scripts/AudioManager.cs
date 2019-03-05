using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // Variables 
    public AudioClip destroyBlockSound;
    public AudioClip placeBlockSound;




    // Play the Destroy Block Audio
    void PlayDestroyBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
    }




    // Play the Place Block Audio
    void PlayPlaceBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
    }




    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced += PlayPlaceBlockSound;
    }




    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced -= PlayPlaceBlockSound;
    }
}
