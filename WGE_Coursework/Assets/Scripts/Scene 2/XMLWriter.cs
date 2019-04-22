using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

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

        xmlWriter.WriteStartElement("NPC Dialogue");

        for (int i = 0; i < npcResponses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response " + i);
            xmlWriter.WriteAttributeString("ID", npcResponses[i].ID);
            xmlWriter.WriteAttributeString("Line", npcResponses[i].line);

            for (int j = 0; j < npcResponses[i].connectedTo.Count; j++)
            {
                xmlWriter.WriteAttributeString("Connected to", npcResponses[i].connectedTo[j].ID);
            }
            
            xmlWriter.WriteEndElement();
        }
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("Player Dialogue");
        for (int i = 0; i < playerResponses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response " + i);
            xmlWriter.WriteAttributeString("ID", playerResponses[i].ID);
            xmlWriter.WriteAttributeString("Line", playerResponses[i].line);
            xmlWriter.WriteAttributeString("Connected to", playerResponses[i].connectedTo[0].ID);
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

    public string LoadFromXML(string fileName, string requiredElement, string line)
    {
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");

        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            if (xmlReader.IsStartElement(""))
            {
               
            }
        }
        return line;
    }
}
