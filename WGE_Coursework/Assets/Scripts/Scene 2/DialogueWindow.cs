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
    static List<ResponseScript> allResponses;
    static List<ResponseScript> playerResponses;
    static List<ResponseScript> npcResponses;
    float currentY = 0;
    static int indentAmount = 0;
    int indentValue = 60;
    
    
    bool newResponse = false;

    [MenuItem("Custom Windows/Dialogue Window")]
    static void Initialise()
    {
        window = (DialogueWindow)EditorWindow.GetWindow(typeof(DialogueWindow));
        window.Show();
        allResponses = new List<ResponseScript>();
        playerResponses = new List<ResponseScript>();
        npcResponses = new List<ResponseScript>();
        xmlWriter = GameObject.Find("XMLObject").GetComponent<XMLWriter>();
        window.AddNewNPCResponse();
    }

    private void OnGUI()
    {
        indentAmount = 0;
        currentY = 30;
        // Set up text style to use for headers
        GUIStyle HeaderTextStyle = new GUIStyle();
        HeaderTextStyle.fontSize = 15;
        HeaderTextStyle.fontStyle = FontStyle.Italic;
        HeaderTextStyle.alignment = TextAnchor.MiddleCenter;

        // The file naming header and text field
        EditorGUILayout.LabelField("File", HeaderTextStyle);
        DialogueWindow.fileName = EditorGUI.TextField(new Rect(0, currentY, position.width, 15), label: "Name of XML file", text: DialogueWindow.fileName);
        currentY += 30;

        // The conversation editor header
        EditorGUI.LabelField(new Rect(0, currentY, position.width, 30), "Conversation Editor", HeaderTextStyle);
        currentY += 30;

        PrintNPCLine(npcResponses[0]);

        currentY += 20;

        EditorGUI.LabelField(new Rect(0, currentY, position.width, 30), "Save", HeaderTextStyle);
        currentY += 30;
        // Save to the file
        if (GUI.Button(new Rect(10, currentY, position.width, 15), text: "Save to file"))
        {
            DialogueWindow.xmlWriter.SaveToXML(DialogueWindow.fileName, npcResponses, playerResponses);
        }
        currentY += 30;
        if (GUI.Button(new Rect(10, currentY, position.width, 15), text: "Load from file"))
        {
            Responses loadedResponses = DialogueWindow.xmlWriter.LoadFromXML(DialogueWindow.fileName);
            playerResponses = loadedResponses.playerResponses;
            npcResponses = loadedResponses.NPCresponses;
            Debug.Log("Player responses: " + playerResponses.Count);
            Debug.Log("NPC responses: " + npcResponses.Count);
        }
    }

    private void AddNewPlayerResponse(ResponseScript npcResponse)
    {
        ResponseScript playerResponse = new ResponseScript();
        playerResponses.Add(playerResponse);
        playerResponse.ID = GUID.Generate().ToString();
        npcResponse.connectedTo.Add(playerResponse); // add this Player Response to the NPC Response

        // make a new NPC response to go with this and add it to the new player response
        playerResponse.connectedTo.Add(AddNewNPCResponse()); // make a new NPCresponse for this playerresponse and add to it
    }

    private ResponseScript AddNewNPCResponse()
    {
        ResponseScript npcResponse = new ResponseScript();
        npcResponses.Add(npcResponse);
        npcResponse.ID = GUID.Generate().ToString();

        return npcResponse;
    }
    
    void PrintNPCLine(ResponseScript npcResponse)
    {
        // keep indentAmount so we can go back to that value later
        indentAmount++;
        int currentIndentAmount = indentAmount;

        if (GUI.Button(new Rect(position.width - 50, currentY, 20, 15), "+"))
        {
            AddNewPlayerResponse(npcResponse);
        }
        EditorGUI.LabelField(new Rect((indentAmount - 1) * indentValue, currentY, 50, 15), "NPC ");
        npcResponse.line = EditorGUI.TextField(new Rect(50 + (indentAmount-1) * indentValue, currentY, position.width - (indentAmount * indentValue) - 50, 15), text: npcResponse.line);
        currentY += 30;

        for (int i = 0; i < npcResponse.connectedTo.Count; i++)
        {
            EditorGUI.LabelField(new Rect((indentAmount - 1) * indentValue, currentY, 50, 15), "Player ");
            npcResponse.connectedTo[i].line = EditorGUI.TextField(new Rect(50 + (indentAmount - 1) * indentValue, currentY, position.width - (indentAmount * indentValue) - 50, 15), text: npcResponse.connectedTo[i].line);
            currentY += 30;
            PrintNPCLine(npcResponse.connectedTo[i].connectedTo[0]);
            indentAmount = currentIndentAmount;
        }
    }
}
