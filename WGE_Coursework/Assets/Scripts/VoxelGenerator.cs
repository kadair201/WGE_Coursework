using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class VoxelGenerator : MonoBehaviour
{
    // Variables
    public float texSize = 0.5f;

    public List<string> texNames;
    public List<Vector2> texCoords;

    int numQuads = 0;

    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;

    Mesh mesh;
    MeshCollider meshCollider;
    
    public Dictionary<string, Vector2> texNameCoordDictionary;



    
    public void Initialise()
    {
        // Initialise the mesh
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        // Initialise the lists
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();

        // Create the texture dictionary
        CreateTextureNameCoordDictionary();
    }




    public void UpdateMesh()
    {
        mesh.Clear();

        // Convert the lists to arrays and store them in the mesh
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triIndexList.ToArray();
        mesh.uv = UVList.ToArray();
        // Recalculate the normals
        mesh.RecalculateNormals();

        // Create a mesh collider
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;

        ClearPreviousData();
    }




    // Create Voxel method
    public void CreateVoxel(int x, int y, int z, Vector2 uvCoords)
    {
        // Create the 6 faces
        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);
        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);
        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
    }

    


    // Create Voxel overload
    public void CreateVoxel(int x, int y, int z, string texture)
    {
        // Get the texture coordinates from the dictionary
        Vector2 uvCoords = texNameCoordDictionary[texture];
        // Create the 6 faces
        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);
        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);
        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
    }


    
    ///////////////////////////////////////////////// Create Faces Methods and Overloads \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


    
    // Create Negative X Face method
    public void CreateNegativeXFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Negative X Face overload, accepting a string
    public void CreateNegativeXFace(int x, int y, int z, string tex)
    {
        // Get coordinates from the texture dictionary
        Vector2 uvCoords = texNameCoordDictionary[tex];
        // Add the vertices to the list
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive X Face method
    public void CreatePositiveXFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive X Face overload
    public void CreatePositiveXFace(int x, int y, int z, string tex)
    {
        Vector2 uvCoords = texNameCoordDictionary[tex];
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Negative Y Face method
    public void CreateNegativeYFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Negative Y Face overload
    public void CreateNegativeYFace(int x, int y, int z, string tex)
    {
        Vector2 uvCoords = texNameCoordDictionary[tex];
        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive Y Face method
    public void CreatePositiveYFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive Y Face overload
    public void CreatePositiveYFace(int x, int y, int z, string tex)
    {
        Vector2 uvCoords = texNameCoordDictionary[tex];
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive Z Face method
    public void CreatePositiveZFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Positive Z Face overload
    public void CreatePositiveZFace(int x, int y, int z, string tex)
    {
        Vector2 uvCoords = texNameCoordDictionary[tex];
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Negative Z Face method
    public void CreateNegativeZFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }




    // Create Negative Z Face overload
    public void CreateNegativeZFace(int x, int y, int z, string tex)
    {
        Vector2 uvCoords = texNameCoordDictionary[tex];
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }



    ///////////////////////////////////////////////// End of Create Faces Methods and Overloads \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\



    void AddTriangleIndices()
    {
        // Add the triangle indices to the list
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;
    }




    void AddUVCoords(Vector2 uvCoords)
    {
        // Add to the UV List
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));
    }

    


    void CreateTextureNameCoordDictionary()
    {
        // Create a new instance of the texture dictionary
        texNameCoordDictionary = new Dictionary<string, Vector2>();
        // Check the number of texture names match the number of texture coordinates
        if (texNames.Count == texCoords.Count)
        {
            for (int i = 0; i < texNames.Count; i++)
            {
                texNameCoordDictionary.Add(texNames[i], texCoords[i]);
            }
        }
        else
        {
            // Print that the lists are uneven
            print("texNames and texCoords are uneven");
        }
    }




    void ClearPreviousData()
    {
        vertexList.Clear();
        triIndexList.Clear();
        UVList.Clear();
        numQuads = 0;
    }

}
