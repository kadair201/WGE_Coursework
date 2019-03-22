using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoxelChunk))]
public class VoxelChunkInspector : Editor {

    string filename = "VoxelChunk";

    public override void OnInspectorGUI()
    {
        //casting target to be voxel chunk
        VoxelChunk myTarget = (VoxelChunk)target;
        filename = EditorGUILayout.TextField(filename);

        if (GUILayout.Button("Load from XML"))
        {
            myTarget.LoadChunk(filename);
        }

        if (GUILayout.Button("Save to XML"))
        {
            myTarget.SaveChunk(filename);
        }

        if (GUILayout.Button("Clear Terrain"))
        {
            myTarget.ClearChunk();
        }

    }


}
