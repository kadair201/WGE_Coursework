using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    public int dirtBlocks = 0;
    public int sandBlocks = 0;
    public int grassBlocks = 0;
    public int stoneBlocks = 0;
    public Text inventoryTextGrass;
    public Text inventoryTextDirt;
    public Text inventoryTextSand;
    public Text inventoryTextStone;

	// Use this for initialization
	void Start () {
        DroppedCubeScript.OnEventAddInventory += AddBlockToInventory;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddBlockToInventory(int blockTex)
    {
        switch (blockTex)
        {
            case 1:
                grassBlocks++;
                inventoryTextGrass.text = "Grass blocks: " + grassBlocks;
                break;
            case 2:
                dirtBlocks++;
                inventoryTextDirt.text = "Dirt blocks: " + dirtBlocks;
                break;
            case 3:
                sandBlocks++;
                inventoryTextSand.text = "Sand blocks: " + sandBlocks;
                break;
            case 4:
                stoneBlocks++;
                inventoryTextStone.text = "Stone blocks: " + stoneBlocks;
                break;
        }
    }
}
