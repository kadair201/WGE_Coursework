using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLWriter : MonoBehaviour {

	public void SaveToXML(string fileName, string firstLine, List<ResponseScript> responses)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);

        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("Dialogue");


        xmlWriter.WriteStartElement("NPCFirstLine");
        xmlWriter.WriteString(firstLine);
        xmlWriter.WriteEndElement();

        for (int i=0; i < responses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response " + i);
            xmlWriter.WriteAttributeString("ID", responses[i].ID);
            xmlWriter.WriteAttributeString("Line", responses[i].line);
            //for (int j = 0; j < responses[i].connectedTo.Count; j++)
            //{
                //xmlWriter.WriteAttributeString("Connected to", responses[i].connectedTo[j]);
            //}
            
            xmlWriter.WriteEndElement();
        }

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
