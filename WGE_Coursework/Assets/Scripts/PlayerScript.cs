using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Variables
    bool empty;
    int blockTex;
    public delegate void EventSetBlock(Vector3 index, int blockType);
    public static event EventSetBlock OnEventSetBlock;




    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, false))
            {
                // find the block texture
                Debug.Log("Block type: " + blockTex);
                // set the block type to 0
                OnEventSetBlock(v, 0);
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, true))
            {
                OnEventSetBlock(v, 3);
            }
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
