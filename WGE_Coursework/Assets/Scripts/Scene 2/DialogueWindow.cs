using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : EditorWindow {

    static DialogueWindow window;
    Color color;
    static string fileName;
    static string firstLine;
    static List<string> responses;

	[MenuItem("Custom Windows/Dialogue Window")]
    static void Initialise()
    {
        window = (DialogueWindow)EditorWindow.GetWindow(typeof(DialogueWindow));
        window.Show();
        responses = new List<string>();
    }

    private void OnGUI()
    {
        // Set up text style to use for headers
        GUIStyle TextStyle = new GUIStyle();
        TextStyle.fontSize = 15;
        TextStyle.fontStyle = FontStyle.Italic;
        TextStyle.alignment = TextAnchor.MiddleCenter;

        // The file naming header and text field
        EditorGUILayout.LabelField("File", TextStyle);
        DialogueWindow.fileName = EditorGUILayout.TextField(label:"Name of XML file", text:DialogueWindow.fileName);

        // The conversation editor header
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Conversation Editor", TextStyle);

        DialogueWindow.firstLine = EditorGUILayout.TextField(label: "NPC opening line", text: DialogueWindow.firstLine);
        for (int i = 0; i < responses.Count; i++)
        {
            DialogueWindow.responses[i] = EditorGUILayout.TextField(label: "Response " + (i+1), text: DialogueWindow.responses[i]);
            Repaint();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button(text: "Add Response"))
        {
            responses.Add("");
        }
    }
}
