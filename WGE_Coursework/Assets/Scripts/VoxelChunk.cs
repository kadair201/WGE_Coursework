﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelChunk : MonoBehaviour
{
    // Variables
    VoxelGenerator voxelGenerator;

    int[,,] terrainArray;
    int chunkSize = 16;




    void Start()
    {
        voxelGenerator = GetComponent<VoxelGenerator>();
        terrainArray = new int[chunkSize, chunkSize, chunkSize];

        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();
        voxelGenerator.UpdateMesh();
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

                        print("Create " + tex + " block,");
                    }
                }
            }
        }
    }




    public void SetBlock(Vector3 index, int blockType)
    {
        // Change the block to the required type
        terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;
        // Create a new mesh
        CreateTerrain();
        // Update the mesh
        voxelGenerator.UpdateMesh();
    }

}