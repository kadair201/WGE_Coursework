using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Responses
{
    public List<ResponseScript> playerResponses = new List<ResponseScript>();
    public List<ResponseScript> NPCresponses = new List<ResponseScript>();
}


public class XMLWriter : MonoBehaviour {

	public void SaveToXML(string fileName, List<ResponseScript> npcResponses, List<ResponseScript> playerResponses)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);

        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("Dialogue");

        xmlWriter.WriteStartElement("NPCDialogue");

        for (int i = 0; i < npcResponses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response");
            xmlWriter.WriteAttributeString("ID", npcResponses[i].ID);
            xmlWriter.WriteAttributeString("Line", npcResponses[i].line);
           

            for (int j = 0; j < npcResponses[i].connectedTo.Count; j++)
            {
                xmlWriter.WriteStartElement("Connections");
                xmlWriter.WriteString(npcResponses[i].connectedTo[j].ID);
                xmlWriter.WriteEndElement();
            }
            
            
            xmlWriter.WriteEndElement();
        }
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("PlayerDialogue");
        for (int i = 0; i < playerResponses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response");
            xmlWriter.WriteAttributeString("ID", playerResponses[i].ID);
            xmlWriter.WriteAttributeString("Line", playerResponses[i].line);
            xmlWriter.WriteAttributeString("ConnectedTo", playerResponses[i].connectedTo[0].ID);
            xmlWriter.WriteEndElement();
        }
        xmlWriter.WriteEndElement();

        // End the root element
        xmlWriter.WriteEndElement();

        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();
    }

    public Responses LoadFromXML(string fileName)
    {
        Responses responses = new Responses();

        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        Debug.Log(fileName);
        List<ResponseScript> readResponses = new List<ResponseScript>();

        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            if (xmlReader.IsStartElement("NPCDialogue"))
            {
                XmlReader responseReader = xmlReader.ReadSubtree();

                while (responseReader.Read())
                {
                    if (xmlReader.IsStartElement("Response"))
                    {
                        ResponseScript response = new ResponseScript();
                        response.ID = responseReader["ID"];
                        response.line = responseReader["Line"];

                        if (responseReader.IsStartElement("Connections"))
                        {
                            XmlReader innerReader = responseReader.ReadSubtree();
                            while (innerReader.Read())
                            {
                                if (innerReader.IsStartElement("Connections"))
                                {
                                    response.connectionIDs.Add(innerReader.Value);
                                }
                            }
                            innerReader.Close();
                        }
                        readResponses.Add(response);
                        responses.NPCresponses.Add(response);
                    }
                }

                responseReader.Close();
            }

            if (xmlReader.IsStartElement("PlayerDialogue"))
            {
                XmlReader responseReader = xmlReader.ReadSubtree();

                while (responseReader.Read())
                {
                    if (xmlReader.IsStartElement("Response"))
                    {
                        Debug.Log("Response");
                        ResponseScript response = new ResponseScript();
                        response.ID = responseReader["ID"];
                        response.line = responseReader["Line"];

                        if (responseReader.IsStartElement("Connections"))
                        {
                            XmlReader innerReader = responseReader.ReadSubtree();
                            while (innerReader.Read())
                            {
                                if (innerReader.IsStartElement("Connections"))
                                {
                                    response.connectionIDs.Add(innerReader.Value);
                                }
                            }
                            innerReader.Close();
                        }
                        readResponses.Add(response);
                        responses.playerResponses.Add(response);
                    }
                }

                responseReader.Close();
            }
            
        }

        foreach (ResponseScript response in readResponses)
        {
            foreach (string connectedID in response.connectionIDs)
            {
                ResponseScript newResponse = FindResponseByID(readResponses, connectedID);
                response.connectedTo.Add(newResponse);
            }
        }

        Debug.Log(readResponses.Count);

        return responses;
    }

    public static ResponseScript FindResponseByID(List<ResponseScript> responseList, string ID)
    {
        foreach (ResponseScript response in responseList)
        {
            if (response.ID == ID)
            {
                return response;
            }
        }

        return null;
    }
}
