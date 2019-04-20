using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLWriter : MonoBehaviour {

	public void SaveToXML(string fileName, string firstLine, string firstLineID, List<string> responses)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);

        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("Dialogue");


        xmlWriter.WriteStartElement("NPCFirstLine");
        xmlWriter.WriteAttributeString("Line", firstLine);
        xmlWriter.WriteAttributeString("ID", firstLineID);
        xmlWriter.WriteEndElement();

        for (int i=0; i < responses.Count; i++)
        {
            xmlWriter.WriteStartElement("Response " + i);
            xmlWriter.WriteString(responses[i]);
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
