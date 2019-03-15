using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        VoxelChunk.OnEventDroppedBlock += SpawnDroppedBlock;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnDroppedBlock(int blockType, Vector3 position)
    {
        // find texture

        // instantiate voxel with texture
    }
}
