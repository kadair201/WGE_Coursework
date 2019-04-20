using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : EditorWindow {

    static DialogueWindow window;
    static XMLWriter xmlWriter;
    static ResponseScript responseScript;

    Color color;
    static string fileName;
    static string firstLine;
    static string firstLineID;
    static List<ResponseScript> playerResponses;
    static List<ResponseScript> npcResponses;

    int numberOfPlayerResponses = -1;
    int numberOfNPCResponses = -1;

    [MenuItem("Custom Windows/Dialogue Window")]
    static void Initialise()
    {
        window = (DialogueWindow)EditorWindow.GetWindow(typeof(DialogueWindow));
        window.Show();
        playerResponses = new List<ResponseScript>();
        npcResponses = new List<ResponseScript>();
        xmlWriter = GameObject.Find("XMLObject").GetComponent<XMLWriter>();
        responseScript = GameObject.Find("XMLObject").GetComponent<ResponseScript>();
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
        DialogueWindow.fileName = EditorGUILayout.TextField(label: "Name of XML file", text:DialogueWindow.fileName);
        

        // The conversation editor header
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Conversation Editor", TextStyle);

        numberOfNPCResponses++;
        ResponseScript newNPCresponse;
        npcResponses.Add(newNPCresponse);
        newNPCresponse.line = EditorGUILayout.TextField(label: "NPC opening line", text: newNPCresponse.line);
        newNPCresponse.ID = "NPC1";
        //npcResponses[numberOfNPCResponses].connectedTo.Add(//ID of player response)
        

        for (int i = 0; i < playerResponses.Count; i++)
        {
            //DialogueWindow.playerResponses[i] = EditorGUILayout.TextField(label: "Response " + (i+1), text: DialogueWindow.responses[i]);
            Repaint();
        }

        EditorGUILayout.Space();



        // Add a response
        if (GUILayout.Button(text:"Add Player Response"))
        {
            numberOfPlayerResponses++;
            playerResponses[numberOfPlayerResponses].line = EditorGUILayout.TextField(label: "Response ", text: playerResponses[numberOfPlayerResponses].line);
            playerResponses[numberOfPlayerResponses].ID = "P" + numberOfPlayerResponses.ToString();
            Debug.Log("P" + numberOfPlayerResponses.ToString());
        }
        


        // Save to the file
        if(GUILayout.Button(text:"Save to file"))
        {
            //DialogueWindow.xmlWriter.SaveToXML(DialogueWindow.fileName, DialogueWindow.firstLine, DialogueWindow.firstLineID, DialogueWindow.responses);
        }
    }
}
