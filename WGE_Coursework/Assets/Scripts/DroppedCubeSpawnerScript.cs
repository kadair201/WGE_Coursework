using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeSpawnerScript : MonoBehaviour {

    public GameObject droppedCubePrefab;

	// Use this for initialization
	void Start () {
        VoxelChunk.OnEventDroppedBlock += SpawnDroppedBlock;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnDroppedBlock(int blockType, Vector3 position)
    {
        GameObject droppedCube = Instantiate(droppedCubePrefab, position, Quaternion.identity);

        droppedCube.tag = "DroppedCube";
        droppedCube.transform.position = position + new Vector3(0.25f, 0.25f, 0.25f);
        droppedCube.GetComponent<DroppedCubeScript>().blockTex = blockType;
    }
}
