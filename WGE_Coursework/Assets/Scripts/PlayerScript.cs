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
        if (Input.GetButtonDown("Fire1") && !voxChunk.panelOpen && !invPanelScript.invPanelOpen)
        {
            Vector3 v;
            if (PickBlock(out v, 4, false))
            {
                // set the block type to 0
                OnEventSetBlock(v, 0);
            }
        }
        else if (Input.GetButtonDown("Fire2") && !voxChunk.panelOpen && !invPanelScript.invPanelOpen)
        {
            if (blockCounts[blockNum-1] > 0)
            {
                Vector3 v;
                if (PickBlock(out v, 4, true))
                {
                    // sets a block of type blockNum down
                    OnEventSetBlock(v, blockNum);
                    blockCounts[blockNum-1]--;
                }
            }
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 5, 0);
        }

        if (voxChunk.panelOpen)
        {
            fpcontroller.m_WalkSpeed = 0;
            fpcontroller.m_RunSpeed = 0;
        }
        else
        {
            fpcontroller.m_WalkSpeed = 5;
            fpcontroller.m_RunSpeed = 10;
        }
        
    }


    

    bool PickBlock(out Vector3 v, float dist, bool empty)
    {
        v = new Vector3();
        
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
