using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    int pos = -1;
    Vector3 pos1;
    public int dirtBlocks = 0;
    public int sandBlocks = 0;
    public int grassBlocks = 0;
    public int stoneBlocks = 0;
    public Text inventoryTextGrass;
    public Text inventoryTextDirt;
    public Text inventoryTextSand;
    public Text inventoryTextStone;
    public RawImage hotbarSelector;

	// Use this for initialization
	void Start () {
        DroppedCubeScript.OnEventAddInventory += AddBlockToInventory;
        pos1 = hotbarSelector.GetComponent<RectTransform>().anchoredPosition = new Vector3(-37, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {
        float sw = Input.GetAxis("Mouse ScrollWheel");

        if (sw < 0)
        {
            Debug.Log("Scrolling Down");
            
        }
        else if (sw > 0)
        {
            Debug.Log("Scrolling Up");
        }
	}

    void AddBlockToInventory(int blockTex)
    {
        switch (blockTex)
        {
            case 1:
                grassBlocks++;
                inventoryTextGrass.text = grassBlocks.ToString();
                break;
            case 2:
                dirtBlocks++;
                inventoryTextDirt.text = dirtBlocks.ToString();
                break;
            case 3:
                sandBlocks++;
                inventoryTextSand.text = sandBlocks.ToString();
                break;
            case 4:
                stoneBlocks++;
                inventoryTextStone.text = stoneBlocks.ToString();
                break;
        }
    }
}
