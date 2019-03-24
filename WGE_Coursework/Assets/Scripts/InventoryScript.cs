using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    int pos = 1;
    bool canScrollUp = true;
    bool canScrollDown = true;
    public Text inventoryTextGrass;
    public Text inventoryTextDirt;
    public Text inventoryTextSand;
    public Text inventoryTextStone;
    public GameObject hotbarSelector;
    public PlayerScript playerScript;

	// Use this for initialization
	void Start () {
        DroppedCubeScript.OnEventAddInventory += AddBlockToInventory;
        Debug.Log(hotbarSelector.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        float sw = Input.GetAxis("Mouse ScrollWheel");

        if (sw < 0)
        {
            if (canScrollDown)
            {
                Debug.Log("Scrolling Up");
                hotbarSelector.transform.Translate(Vector3.left * 36);
                pos--;
            }
        }
        else if (sw > 0)
        {
            if (canScrollUp)
            {
                Debug.Log("Scrolling Up");
                hotbarSelector.transform.Translate(Vector3.right * 36);
                pos++;
            }
        }

        if (pos == 1)
        {
            canScrollDown = false;
        }
        else
        {
            canScrollDown = true;
        }

        if (pos == 4)
        {
            canScrollUp = false;
        }
        else
        {
            canScrollUp = true;
        }

        playerScript.blockNum = pos;
        inventoryTextGrass.text = playerScript.blockCounts[0].ToString();
        inventoryTextDirt.text = playerScript.blockCounts[1].ToString();
        inventoryTextSand.text = playerScript.blockCounts[2].ToString();
        inventoryTextStone.text = playerScript.blockCounts[3].ToString();
    }

    void AddBlockToInventory(int blockTex)
    {
        switch (blockTex)
        {
            case 1:
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
