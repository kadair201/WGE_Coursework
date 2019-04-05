using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryPanelScript : MonoBehaviour {

    // Variables 
    Image itemSprite;
    Text itemNameText;
    Text itemCountText;

    public GameObject inventoryPanel;
    public VoxelChunk voxChunk;
    public FirstPersonController fpscript;
    public PlayerScript playerScript;

    //public string itemName;
    //public int itemAmount;
    public bool invPanelOpen = false;
    public bool isSortedHighToLow = false;

    int[] blockAmounts;
    public Sprite[] blockImage;
    public GameObject[] panel;



    // Use this for initialization
    void Start ()
    {
        inventoryPanel.SetActive(false);
        invPanelOpen = false;
        blockAmounts = new int[4];
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.I))
        {
            if (invPanelOpen)
            {
                invPanelOpen = false;
                // if the inventory panel is open, close it
                inventoryPanel.SetActive(false);
                // freeze the cursor
                voxChunk.LockCursor();
            }
            else
            {
                invPanelOpen = true;
                // if the inventory panel is closed, open it
                inventoryPanel.SetActive(true);
                // unfreeze the cursor
                voxChunk.UnlockCursor();
            }
        }

        if (!invPanelOpen && !voxChunk.panelOpen)
        {
            fpscript.m_RunSpeed = 10;
            fpscript.m_WalkSpeed = 5;
        }
        else if (invPanelOpen || voxChunk.panelOpen)
        {
            fpscript.m_RunSpeed = 0;
            fpscript.m_WalkSpeed = 0;
        }
	}

    public void SortAlphabetically()
    {
        
    }



    ///////////////////////////////////////////////////////////////////////////// Start of Sorting By Amount ///////////////////////////////////////////////////////////////////////////////////////



    public void SortByAmount()
    {
        // Populate blockAmounts array
        for (int i = 0; i < 4; i++)
        {
            blockAmounts[i] = playerScript.blockCounts[i];
        }
        
        if (isSortedHighToLow)
        {
            SortLowToHigh(blockAmounts);
        }
        else
        {
            SortHighToLow(blockAmounts);
        }
    }



    int[] SortLowToHigh(int[] amounts)
    {
        isSortedHighToLow = false;
        // if there is only one number, don't try to split into two arrays
        if (amounts.Length <= 1) return amounts;

        int arrayLength = amounts.Length / 2;
        int[] firstHalf = new int[arrayLength];
        int[] secondHalf = new int[arrayLength];

        for (int i = 0; i < amounts.Length; i++)
        {
            if (i <= arrayLength - 1)
            {
                firstHalf[i] = amounts[i];
                //Debug.Log("number " + firstHalf[i] + " added to firstHalf");
            }
            else if (i > arrayLength - 1)
            {
                secondHalf[i - arrayLength] = amounts[i];
                //Debug.Log("number " + secondHalf[i - arrayLength] + " added to secondHalf");
            }
        }

        firstHalf = SortLowToHigh(firstHalf);
        secondHalf = SortLowToHigh(secondHalf);

        amounts = MergeLowToHigh(firstHalf, secondHalf);

        if (amounts.Length == 4)
        {
            string[] blockName = { "Grass", "Dirt", "Sand", "Stone" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (playerScript.blockCounts[i] == amounts[j])
                    {
                        panel[j].GetComponent<Image>().sprite = blockImage[i];
                        panel[j].GetComponentInChildren<Text>().text = blockName[i] + ": " + amounts[j];
                    }
                }
            }
        }
        return amounts;
    }



    int[] MergeLowToHigh(int[] a, int[] b)
    {
        int[] merged = new int[a.Length + b.Length];
        int i, j, m;
        i = j = m = 0;

        while (i < a.Length && j < b.Length)
        {
            if (a[i] <= b[j])
            {
                merged[m] = a[i];
                i++;
                m++;
            }
            else
            {
                merged[m] = b[j];
                j++;
                m++;
            }
        }

        if (i < a.Length)
        {
            // add the rest of the elements in a to the end of merged
            for (int k = i; k < a.Length; k++)
            {
                merged[m] = a[k];
                m++;
            }
        }
        else
        {
            // add the rest of the elements in b to the end of merged
            for (int k = j; k < b.Length; k++)
            {
                merged[m] = b[k];
                m++;
            }
        }

        return merged;
    }



    int[] SortHighToLow(int[] amounts)
    {
        isSortedHighToLow = true;
        // if there is only one number, don't try to split into two arrays
        if (amounts.Length <= 1) return amounts;

        int arrayLength = amounts.Length / 2;
        int[] firstHalf = new int[arrayLength];
        int[] secondHalf = new int[arrayLength];

        for (int i = 0; i < amounts.Length; i++)
        {
            if (i <= arrayLength - 1)
            {
                firstHalf[i] = amounts[i];
                //Debug.Log("number " + firstHalf[i] + " added to firstHalf");
            }
            else if (i > arrayLength - 1)
            {
                secondHalf[i - arrayLength] = amounts[i];
                //Debug.Log("number " + secondHalf[i - arrayLength] + " added to secondHalf");
            }
        }

        firstHalf = SortHighToLow(firstHalf);
        secondHalf = SortHighToLow(secondHalf);

        amounts = MergeHighToLow(firstHalf, secondHalf);

        if (amounts.Length == 4)
        {
            string[] blockName = { "Grass", "Dirt", "Sand", "Stone" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (playerScript.blockCounts[i] == amounts[j])
                    {
                        panel[j].GetComponent<Image>().sprite = blockImage[i];
                        panel[j].GetComponentInChildren<Text>().text = blockName[i] + ": " + amounts[j];
                    }
                }
            }
        }
        return amounts;
    }



    int[] MergeHighToLow(int[] a, int[] b)
    {
        int[] merged = new int[a.Length + b.Length];
        int i, j, m;
        i = j = m = 0;

        while (i < a.Length && j < b.Length)
        {
            if (a[i] >= b[j])
            {
                merged[m] = a[i];
                i++;
                m++;
            }
            else
            {
                merged[m] = b[j];
                j++;
                m++;
            }
        }

        if (i < a.Length)
        {
            // add the rest of the elements in a to the end of merged
            for (int k = i; k < a.Length; k++)
            {
                merged[m] = a[k];
                m++;
            }
        }
        else
        {
            // add the rest of the elements in b to the end of merged
            for (int k = j; k < b.Length; k++)
            {
                merged[m] = b[k];
                m++;
            }
        }

        return merged;
    }



    ///////////////////////////////////////////////////////////////////////////// End of Sorting By Amount ///////////////////////////////////////////////////////////////////////////////////////



    public void SearchByName()
    {

    }
}
