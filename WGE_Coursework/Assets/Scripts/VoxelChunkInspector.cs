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
            myTarget.LoadSave(filename);
        }

        if (GUILayout.Button("Save to XML"))
        {
            myTarget.SaveFile(filename);
        }

        if (GUILayout.Button("Clear Terrain"))
        {
            myTarget.ClearFile();
        }
    }


}
