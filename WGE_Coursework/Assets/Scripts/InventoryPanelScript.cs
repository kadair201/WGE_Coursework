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
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public VoxelChunk voxChunk;
    public FirstPersonController fpscript;
    public PlayerScript playerScript;

    public string itemName;
    public int itemAmount;
    public bool invPanelOpen = false;



    // Use this for initialization
    void Start ()
    {
        inventoryPanel.SetActive(false);
        invPanelOpen = false;
        itemSprite = panel1.GetComponentInChildren<Image>();
        
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
        //
    }

    public void SortByAmount()
    {

    }

    public void SearchByName()
    {

    }
}
