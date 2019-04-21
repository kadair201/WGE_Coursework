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
    float currentY = 0;
    static int indentAmount = 0;
    
    
    bool newResponse = false;

    [MenuItem("Custom Windows/Dialogue Window")]
    static void Initialise()
    {
        window = (DialogueWindow)EditorWindow.GetWindow(typeof(DialogueWindow));
        window.Show();
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
        EditorGUI.LabelField(new Rect(0, currentY, position.width, 30), "Conversation Start", HeaderTextStyle);
        currentY += 30;

        PrintNPCLine(npcResponses[0]);

        currentY += 20;
        
        // Save to the file
        if (GUI.Button(new Rect(10, currentY, position.width, 15), text: "Save to file"))
        {
            DialogueWindow.xmlWriter.SaveToXML(DialogueWindow.fileName, DialogueWindow.firstLine, DialogueWindow.playerResponses);
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

        if (GUI.Button(new Rect(position.width - 50, currentY, 50, 15), "Add"))
        {
            AddNewPlayerResponse(npcResponse);
        }

        Debug.Log(indentAmount);
        npcResponse.line = EditorGUI.TextField(new Rect(indentAmount * 40, currentY, position.width - (indentAmount * 40) - 50, 15), label: "NPC " + npcResponse.ID.Substring(npcResponse.ID.Length - 3) + "Ind. " + indentAmount, text: npcResponse.line);
        currentY += 60;

        for (int i = 0; i < npcResponse.connectedTo.Count; i++)
        {
            npcResponse.connectedTo[i].line = EditorGUI.TextField(new Rect(10 + (indentAmount * 40), currentY, position.width - (indentAmount * 40) - 50, 15), label: "PLAYER " + npcResponse.ID.Substring(npcResponse.ID.Length - 3) + "Ind. " + indentAmount, text: npcResponse.connectedTo[i].line);
            currentY += 30;
            //Debug.Log("Player response connected to: " + npcResponse.connectedTo[i].connectedTo.Count);
            //indentAmount++;
            PrintNPCLine(npcResponse.connectedTo[i].connectedTo[0]);
            indentAmount = currentIndentAmount;
        }
    }
}
