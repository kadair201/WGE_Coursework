using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {

    public XMLWriter xmlWriter;
    public CameraScript camScript;
    public List<ResponseScript> playerResponses;
    public List<ResponseScript> npcResponses;
    public string filename;
    public GameObject npcBox;
    public Text npcText;
    public GameObject playerBox;
    public Text playerText;
    public Button buttonPrefab;
    int npcLineNumber = 0;

    private void Start()
    {
        playerResponses = new List<ResponseScript>();
        npcResponses = new List<ResponseScript>();
        npcBox.SetActive(false);
        playerBox.SetActive(false);
    }

    public void BeginDialogue()
    {
        npcBox.SetActive(true);
        Responses loadedResponses = xmlWriter.LoadFromXML(filename);
        playerResponses = loadedResponses.playerResponses;
        npcResponses = loadedResponses.NPCresponses;

        npcLineNumber = 0;
        npcText.text = npcResponses[npcLineNumber].line;
        
    }

    private void Update()
    {
        if (camScript.zooming)
        {
            if (camScript.lerpSubject == GameObject.Find("NPC"))
            {
                npcBox.SetActive(true);
                playerBox.SetActive(false);
            }
            else
            {
                npcBox.SetActive(false);
            }
        }
        
    }

    public void ShowPlayerOptions()
    {
        playerBox.SetActive(true);
        int responseCount = 0;
        int currentY = 30;

        // find all connected responses
        foreach (ResponseScript connectedResponses in npcResponses[npcLineNumber].connectedTo)
        {
            //Debug.Log(connectedResponses.line);
            responseCount++;
        }
        // display them as buttons

        for (int i = 0; i < npcResponses[npcLineNumber].connectedTo.Count; i++)
        {
            if (npcResponses[npcLineNumber].connectedTo[i].line != "")
            {
                Button spawnedButton = Instantiate(buttonPrefab);
                spawnedButton.name = "ResponseButton" + i;
                spawnedButton.transform.SetParent(GameObject.Find("PlayerTextBox").transform);
                spawnedButton.transform.localPosition = new Vector3(0, currentY, 0);
                currentY -= 30;
                spawnedButton.GetComponentInChildren<Text>().text = npcResponses[npcLineNumber].connectedTo[i].line;
                ResponseScript currentResponse = new ResponseScript();
                currentResponse = npcResponses[npcLineNumber].connectedTo[i];
                spawnedButton.onClick.AddListener(() => NextNPCLine(currentResponse, npcResponses[npcLineNumber].connectedTo.Count));
            }
            else
            {
                camScript.ZoomOut();
                npcBox.SetActive(false);
                playerBox.SetActive(false);
                break;
            }
        }
    }

    void NextNPCLine(ResponseScript playerLine, int numberOfButtons)
    {
        if (playerLine.connectedTo[0].line == "" || playerLine.line == "")
        {
            camScript.ZoomOut();
            npcBox.SetActive(false);
            playerBox.SetActive(false);
            return;
        }

        // Destroy the buttons from the last response
        for (int i = 0; i < numberOfButtons; i++)
        {
            Destroy(GameObject.Find("ResponseButton" + i));
        }

        // find current player line 
        //Debug.Log(playerLine.connectedTo[0].line);
        // find current player line connection 
        camScript.lerpSubject = GameObject.Find("NPC");
        npcText.text = playerLine.connectedTo[0].line;

        for (int i = 0; i < npcResponses.Count; i++)
        {
            if (npcResponses[i].line == playerLine.connectedTo[0].line)
            {
                npcLineNumber = i;
            }
 
        }
    }
}
