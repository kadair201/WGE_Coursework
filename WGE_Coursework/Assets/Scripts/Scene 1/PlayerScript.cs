using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

    // Variables
    bool empty;
    public int blockNum;
    public int[] blockCounts;
    public delegate void EventSetBlock(Vector3 index, int blockType);
    public static event EventSetBlock OnEventSetBlock;
    public Camera mainCam;
    public VoxelChunk voxChunk;
    public InventoryPanelScript invPanelScript;
    public FirstPersonController fpcontroller;




    // Update is called once per frame
    void Update ()
    {
        // if left mouse button pressed and no panels are open
        if (Input.GetButtonDown("Fire1") && !voxChunk.panelOpen && !invPanelScript.invPanelOpen)
        {
            Vector3 v;
            if (PickBlock(out v, 4, false))
            {
                // set the block type to 0, i.e. destroy the block
                OnEventSetBlock(v, 0);
            }
        }
        // if right mouse button pressed and no panels are open
        else if (Input.GetButtonDown("Fire2") && !voxChunk.panelOpen && !invPanelScript.invPanelOpen)
        {
            // check there is at least 1 block in the inventory of the selected type
            if (blockCounts[blockNum-1] > 0)
            {
                Vector3 v;
                if (PickBlock(out v, 4, true))
                {
                    // sets a block of type blockNum down
                    OnEventSetBlock(v, blockNum);
                    // subtract one from the inventory
                    blockCounts[blockNum-1]--;
                }
            }
        }

        // if the player falls off the map, teleport to the terrain again
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }


    

    bool PickBlock(out Vector3 v, float dist, bool empty)
    {
        v = new Vector3();
        
        // instantiate a new raycast from the centre of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, dist))
        {
            if (empty == true)
            {
                v = hit.point + hit.normal / 2;
            }
            else
            {
                
                v = hit.point - hit.normal / 2;
            }
            // round down to get the index of the empty
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }
        return false;
    }

}
