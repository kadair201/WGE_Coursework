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
        // spawn a cube from the prefab
        GameObject droppedCube = Instantiate(droppedCubePrefab, position, Quaternion.identity);

        // tag the cube, position it in centre of the destroyed block
        droppedCube.tag = "DroppedCube";
        droppedCube.transform.position = position + new Vector3(0.25f, 0.25f, 0.25f);

        // assign the blockType integer to the blockTex variable in DroppedCubeScript
        droppedCube.GetComponent<DroppedCubeScript>().blockTex = blockType;
    }
}
