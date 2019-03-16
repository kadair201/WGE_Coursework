using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeScript : MonoBehaviour {

    public int blockTex;
    string blockTexName;

    public delegate void EventAddInventory(int blockType);
    public static event EventAddInventory OnEventAddInventory;

    GameObject player;
    VoxelGenerator voxelGenerator;
    Vector3 direction;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        voxelGenerator = GetComponent<VoxelGenerator>();
        switch (blockTex)
        {
            case 1:
                blockTexName = "Grass";
                break;
            case 2:
                blockTexName = "Dirt";
                break;
            case 3:
                blockTexName = "Sand";
                break;
            case 4:
                blockTexName = "Stone";
                break;
            default:
                blockTexName = "Grass";
                break;
        }
        voxelGenerator.Initialise();
        voxelGenerator.CreateVoxel(0, 0, 0, blockTexName);
        voxelGenerator.UpdateMesh();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        direction = (player.transform.position - transform.position);
        direction.Normalize();
        rb.AddForce(direction, ForceMode.Impulse);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag ==  "Player")
        {
            OnEventAddInventory(blockTex);
            Destroy(gameObject);
        }
    }
}
