using UnityEditor;
using UnityEngine;

public class ExampleEditorWindowScript : EditorWindow {

    static ExampleEditorWindowScript window;

	[MenuItem("CustomWindows/ExampleWindow")]
    static void Initialise()
    {
        window = (ExampleEditorWindowScript)EditorWindow.GetWindow(typeof(ExampleEditorWindowScript));
        window.Show();
    }
}
