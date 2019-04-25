using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    int pos = 1;
    bool grassSelected = true;
    bool dirtSelected = false;
    bool sandSelected = false;
    bool stoneSelected = false;
    public Text inventoryTextGrass;
    public Text inventoryTextDirt;
    public Text inventoryTextSand;
    public Text inventoryTextStone;
    public RawImage grass;
    public RawImage dirt;
    public RawImage sand;
    public RawImage stone;
    public PlayerScript playerScript;




	// Use this for initialization
	void Start () {
        DroppedCubeScript.OnEventAddInventory += AddBlockToInventory;
	}
	



	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pos = 1;
            Color colour = grass.GetComponent<RawImage>().color;
            colour.a = 1;
            grass.GetComponent<RawImage>().color = colour;
            grassSelected = true;
            dirtSelected = false;
            sandSelected = false;
            stoneSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pos = 2;
            Color colour = dirt.GetComponent<RawImage>().color;
            colour.a = 1;
            dirt.GetComponent<RawImage>().color = colour;
            grassSelected = false;
            dirtSelected = true;
            sandSelected = false;
            stoneSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pos = 3;
            Color colour = sand.GetComponent<RawImage>().color;
            colour.a = 1;
            sand.GetComponent<RawImage>().color = colour;
            grassSelected = false;
            dirtSelected = false;
            sandSelected = true;
            stoneSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pos = 4;
            Color colour = stone.GetComponent<RawImage>().color;
            colour.a = 1;
            stone.GetComponent<RawImage>().color = colour;
            grassSelected = false;
            dirtSelected = false;
            sandSelected = false;
            stoneSelected = true;
        }

        if (!grassSelected)
        {
            Color colour = grass.GetComponent<RawImage>().color;
            colour.a = 0.5f;
            grass.GetComponent<RawImage>().color = colour;
        }

        if (!dirtSelected)
        {
            Color colour = dirt.GetComponent<RawImage>().color;
            colour.a = 0.5f;
            dirt.GetComponent<RawImage>().color = colour;
        }

        if (!sandSelected)
        {
            Color colour = sand.GetComponent<RawImage>().color;
            colour.a = 0.5f;
            sand.GetComponent<RawImage>().color = colour;
        }

        if (!stoneSelected)
        {
            Color colour = stone.GetComponent<RawImage>().color;
            colour.a = 0.5f;
            stone.GetComponent<RawImage>().color = colour;
        }

        playerScript.blockNum = pos;
        inventoryTextGrass.text = playerScript.blockCounts[0].ToString();
        inventoryTextDirt.text = playerScript.blockCounts[1].ToString();
        inventoryTextSand.text = playerScript.blockCounts[2].ToString();
        inventoryTextStone.text = playerScript.blockCounts[3].ToString();
    }




    void AddBlockToInventory(int blockTex)
    {
        // find which inventory count to add to
        switch (blockTex)
        {
            case 1:
                // add to the inventory count
                playerScript.blockCounts[0]++;
                
                break;
            case 2:
                playerScript.blockCounts[1]++;
                
                break;
            case 3:
                playerScript.blockCounts[2]++;
                
                break;
            case 4:
                playerScript.blockCounts[3]++;
                
                break;
        }
    }
}
