using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : EditorWindow
{

    static DialogueWindow window;
    static XMLWriter xmlWriter;
    static ResponseScript responseScript;

    Color color;
    static string fileName;
    static string firstLine;
    static string firstLineID;
    static List<ResponseScript> playerResponses;
    static List<ResponseScript> npcResponses;
    
    
    bool newResponse = false;

    [MenuItem("Custom Windows/Dialogue Window")]
    static void Initialise()
    {
        window = (DialogueWindow)EditorWindow.GetWindow(typeof(DialogueWindow));
        window.Show();
        playerResponses = new List<ResponseScript>();
        npcResponses = new List<ResponseScript>();
        xmlWriter = GameObject.Find("XMLObject").GetComponent<XMLWriter>();
    }

    private void OnGUI()
    {
        // Set up text style to use for headers
        GUIStyle HeaderTextStyle = new GUIStyle();
        HeaderTextStyle.fontSize = 15;
        HeaderTextStyle.fontStyle = FontStyle.Italic;
        HeaderTextStyle.alignment = TextAnchor.MiddleCenter;

        // The file naming header and text field
        EditorGUILayout.LabelField("File", HeaderTextStyle);
        DialogueWindow.fileName = EditorGUILayout.TextField(label: "Name of XML file", text: DialogueWindow.fileName);


        // The conversation editor header
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Conversation Start", HeaderTextStyle);

        firstLine = EditorGUILayout.TextField(label: "NPC opening line", text: firstLine);
        EditorGUILayout.Space();

        for (int i = 0; i < playerResponses.Count; i++)
        {
            playerResponses[i].line = EditorGUILayout.TextField(label: "Player response " + (i + 1), text: playerResponses[i].line);
            string npcAnswer = "";
            EditorGUILayout.BeginHorizontal();
            EditorGUI.indentLevel++;
            npcAnswer = EditorGUI.TextField(new Rect(100, 110, position.width, 15), label: "NPC ", text: npcAnswer);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            if (GUI.Button(new Rect(10, 110, 50, 15), "Add"))
            {
                Debug.Log("Pressed");
                EditorGUILayout.Space();
                string newPlayerField = "";
                newPlayerField = EditorGUILayout.TextField(label: "Player ", text: newPlayerField);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUILayout.Space();

        
        // Add a response
        if (GUILayout.Button(text: "Add Player Response"))
        {
            AddNewPlayerResponse();
        }

        /*
        EditorGUILayout.LabelField("NPC Responses", HeaderTextStyle);
        int index = 0;

        for (int j = 0; j < playerResponses.Count; j++)
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Choose a player response");
            string[] popupOptions = new string[playerResponses.Count];
            for (int i = 0; i < playerResponses.Count; i++)
            {
                popupOptions[i] = playerResponses[i].line;
            }
            index = EditorGUILayout.Popup(index, popupOptions);
            GUILayout.EndHorizontal();

            AddNewNPCResponse();

            npcResponses[j].line = EditorGUILayout.TextField(label: "NPC response", text: npcResponses[j].line);
        }*/




        // Save to the file
        if (GUILayout.Button(text: "Save to file"))
        {
            //DialogueWindow.xmlWriter.SaveToXML(DialogueWindow.fileName, DialogueWindow.firstLine, DialogueWindow.firstLineID, DialogueWindow.responses);
        }
    }

    private void AddNewPlayerResponse()
    {
        ResponseScript responseScript = new ResponseScript();
        playerResponses.Add(responseScript);
        responseScript.ID = GUID.Generate().ToString();
    }

    private void AddNewNPCResponse()
    {
        ResponseScript responseScript = new ResponseScript();
        npcResponses.Add(responseScript);
        responseScript.ID = GUID.Generate().ToString();
        
    }
}
