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
    public InputField searchBar;
    public string search;

    public bool invPanelOpen = false;
    public bool isSortedHighToLow = false;

    int[] blockAmounts;
    public Sprite[] blockImage;
    public GameObject[] panel;
    public string[] blockName;



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
        // Populate blockAmounts array
        for (int i = 0; i < 4; i++)
        {
            blockAmounts[i] = playerScript.blockCounts[i];
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
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
                SortByAmount();
                searchBar.text = "";
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

        search = searchBar.text;
        SearchByName(search.ToLower());
    }

    public void SortAlphabetically()
    {
        
    }



    ///////////////////////////////////////////////////////////////////////////// Start of Sorting By Amount ///////////////////////////////////////////////////////////////////////////////////////



    public void SortByAmount()
    {
        if (isSortedHighToLow)
        {
            isSortedHighToLow = false;
        }
        else
        {
            isSortedHighToLow = true;
        }

        Sort(blockAmounts);
    }



    int[] Sort(int[] amounts)
    {
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

        firstHalf = Sort(firstHalf);
        secondHalf = Sort(secondHalf);
        
        if (!isSortedHighToLow)
        {
            amounts = MergeLowToHigh(firstHalf, secondHalf);
        }
        else
        {
            amounts = MergeHighToLow(firstHalf, secondHalf);
        }

        // once the sorting is finished
        if (amounts.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // compare the original block positions with the new positions
                    if (playerScript.blockCounts[i] == amounts[j])
                    {
                        // change the image to reflect the new block in that position
                        panel[j].GetComponent<Image>().sprite = blockImage[i];
                        // change the text 
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



    public void SearchByName(string playerInput)
    { 
        for (int i = 0; i < blockName.Length; i++)
        {
            if (playerInput != "")
            {
                if (!panel[i].GetComponentInChildren<Text>().text.ToLower().Contains(playerInput))
                {
                    // Reduce opacity of all panels that do not match the player input
                    Color color = panel[i].GetComponent<Image>().color;
                    color.a = 0.1f;
                    panel[i].GetComponent<Image>().color = color;
                }
                else
                {
                    // if the item matches the player input, make opacity 1
                    Color color = panel[i].GetComponent<Image>().color;
                    color.a = 1;
                    panel[i].GetComponent<Image>().color = color;
                }
            }
            else
            {
                // if there is no input, make all the panels' opacity 1
                Color color = panel[i].GetComponent<Image>().color;
                color.a = 1;
                panel[i].GetComponent<Image>().color = color;
            }
            
        }
    }
}
