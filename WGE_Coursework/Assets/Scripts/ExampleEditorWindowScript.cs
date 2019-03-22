using UnityEditor;
using UnityEngine;

public class ExampleEditorWindowScript : EditorWindow {

    static ExampleEditorWindowScript window;
    Color color;

	[MenuItem("CustomWindows/ExampleWindow")]
    static void Initialise()
    {
        window = (ExampleEditorWindowScript)EditorWindow.GetWindow(typeof(ExampleEditorWindowScript));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Change the colour of the selected object");
        using (var horizontalScope = new EditorGUILayout.HorizontalScope())
        {
            color = EditorGUILayout.ColorField("Colour", color);
            if (GUILayout.Button("Change colour"))
            {
                foreach (var o in Selection.objects)
                {
                    GameObject go = (GameObject)o;
                    if (go != null)
                    {
                        go.GetComponent<Renderer>().sharedMaterial.color = color;
                    }
                }

                if (Selection.objects.Length < 1)
                {
                    Debug.Log("Must select a game object");
                }
            }

        }
    }
}
