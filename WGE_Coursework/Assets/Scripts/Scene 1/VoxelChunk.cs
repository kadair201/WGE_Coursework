using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class VoxelChunk : MonoBehaviour
{
    // Variables
    VoxelGenerator voxelGenerator;
    public GameObject player;
    public MouseLook mouseLook;
    public bool panelOpen = false;
    GameObject loadPanel;
    InputField loadFileName;

    int[,,] terrainArray;
    int chunkSize = 16;
    
    public delegate void EventBlockChanged();
    public delegate void EventBlockChangedWithType(int blockType);
    public delegate void EventDroppedBlock(int blockType, Vector3 position);
    public static event EventBlockChangedWithType OnEventBlockChanged;
    public static event EventDroppedBlock OnEventDroppedBlock;



    void Start()
    {
        voxelGenerator = GetComponent<VoxelGenerator>();
        terrainArray = new int[chunkSize, chunkSize, chunkSize];
        loadPanel = GameObject.Find("LoadPanel");
        loadFileName = loadPanel.GetComponentInChildren<InputField>();

        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();
        voxelGenerator.UpdateMesh();

        PlayerScript.OnEventSetBlock += SetBlock;
        player = GameObject.Find("Player");
        loadPanel.SetActive(false);
        panelOpen = false;
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<FirstPersonController>().frozen = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<FirstPersonController>().frozen = true;
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, "Save", player.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (!panelOpen)
            {
                loadPanel.SetActive(true);
                UnlockCursor();
                panelOpen = true;
            }
            else
            {
                loadPanel.SetActive(false);
                LockCursor();
                panelOpen = false;
            }
        }

    }




    void InitialiseTerrain()
    {
        // For every voxel (width)
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // For every voxel (height)
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // For every voxel (depth)
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    // If on the 4th layer
                    if (y == 3)
                    {
                        terrainArray[x, y, z] = 1;
                        terrainArray[0, 3, 1] = 4;
                        terrainArray[0, 3, 2] = 4;
                        terrainArray[0, 3, 3] = 4;
                        terrainArray[1, 3, 3] = 4;
                        terrainArray[1, 3, 4] = 4;
                        terrainArray[2, 3, 4] = 4;
                        terrainArray[3, 3, 4] = 4;
                        terrainArray[4, 3, 4] = 4;
                        terrainArray[5, 3, 4] = 4;
                        terrainArray[5, 3, 3] = 4;
                        terrainArray[5, 3, 2] = 4;
                        terrainArray[6, 3, 2] = 4;
                        terrainArray[7, 3, 2] = 4;
                        terrainArray[8, 3, 2] = 4;
                        terrainArray[9, 3, 2] = 4;
                        terrainArray[10, 3, 2] = 4;
                        terrainArray[11, 3, 2] = 4;
                        terrainArray[12, 3, 2] = 4;
                        terrainArray[13, 3, 2] = 4;
                        terrainArray[13, 3, 3] = 4;
                        terrainArray[14, 3, 3] = 4;
                        terrainArray[15, 3, 3] = 4;
                    }
                    // If below the 4th layer
                    else if (y < 3)
                    {
                        terrainArray[x, y, z] = 2;
                    }
                }
            }
        }
    }


    

    void CreateTerrain()
    {
        // For every voxel (width)
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // For every voxel (height)
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // For every voxel (depth)
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    // If the voxel isn't empty
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;

                        // Assign texture name depending on value
                        switch (terrainArray[x, y, z])
                        {
                            case 1:
                                tex = "Grass";
                                break;
                            case 2:
                                tex = "Dirt";
                                break;
                            case 3:
                                tex = "Sand";
                                break;
                            case 4:
                                tex = "Stone";
                                break;
                            default:
                                tex = "Grass";
                                break;
                        }

                        // If we need to draw the negative x face
                        if (x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }

                        // If we need to draw the positive x face
                        if (x == terrainArray.GetLength(0) - 1 || terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }

                        // If we need to draw the negative y face
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }

                        // If we need to draw the positive y face
                        if (y == terrainArray.GetLength(1) - 1 || terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);
                        }

                        // If we need to draw the negative z face
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        // If we need to draw the positive z face
                        if (z == terrainArray.GetLength(2) - 1 || terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                    }
                }
            }
        }
    }




    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x >= 0 && index.x <= terrainArray.GetLength(0)) && (index.y >= 0 && 
            index.y <= terrainArray.GetLength(1)) && (index.z >= 0 && index.z <= terrainArray.GetLength(2)))
        { 
            // If the block is destroyed, spawn the collectable block
            if (blockType == 0)
            {
                OnEventDroppedBlock(terrainArray[(int)index.x, (int)index.y, (int)index.z], index);
            }
            // Change the block to the required type
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;
            // Create the new mesh
            CreateTerrain();
            // Update the mesh data
            GetComponent<VoxelGenerator>().UpdateMesh();

            OnEventBlockChanged(blockType);
        }
    }




    public void LoadChunk(string filename)
    {
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, filename);
        CreateTerrain();
        voxelGenerator.UpdateMesh();
    }



    public void SaveChunk(string filename)
    {
        XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, "VoxelChunk", player.transform.position);
    }




    public void ClearChunk()
    {
        terrainArray = new int[chunkSize, chunkSize, chunkSize];
        InitialiseTerrain();
        CreateTerrain();
        voxelGenerator.UpdateMesh();
    }




    public void LoadButtonPressed()
    {
        string playerFileName = loadFileName.text;
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, playerFileName);
        if (terrainArray != null)
        {
            // Draw the correct faces
            CreateTerrain();
            // Update mesh info
            voxelGenerator.UpdateMesh();
            LockCursor();
            loadPanel.SetActive(false);
            panelOpen = false;
        }
        else
        {
            Debug.Log("No file found.");
        }
    }
}
