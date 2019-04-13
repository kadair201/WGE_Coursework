using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLVoxelFileWriter
{ 

    // Write a voxel chunk to XML file
    public static void SaveChunkToXMLFile(int[,,] voxelArray, string fileName, Vector3 position)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        // Create a write instance
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);
        
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("VoxelChunk");

        // iterate through all array elements
        for (int x = 0; x < voxelArray.GetLength(0); x++)
        {
            for (int y = 0; y < voxelArray.GetLength(1); y++)
            {
                for (int z = 0; z < voxelArray.GetLength(1); z++)
                {
                    if (voxelArray[x, y, z] != 0)
                    {
                        // Create a single voxel element
                        xmlWriter.WriteStartElement("Voxel");

                        // Write an attribute to store the x index
                        xmlWriter.WriteAttributeString("x", x.ToString());
                        // Write an attribute to store the y index
                        xmlWriter.WriteAttributeString("y", y.ToString());
                        // Write an attribute to store the z index
                        xmlWriter.WriteAttributeString("z", z.ToString());

                        // Store the voxel type
                        xmlWriter.WriteString(voxelArray[x, y, z].ToString());

                        // End the voxel element
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        xmlWriter.WriteStartElement("Position");
        xmlWriter.WriteAttributeString("x", position.x.ToString());
        xmlWriter.WriteAttributeString("y", position.y.ToString());
        xmlWriter.WriteAttributeString("z", position.z.ToString());

        // End the root element
        xmlWriter.WriteEndElement();
        
        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();

    }

    // Read a voxel chunk from XML file
    public static int[,,] LoadChunkFromXMLFile(int size, string fileName)
    {
        int[,,] voxelArray = new int[size, size, size];
        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            // Check if this node is a Voxel element
            if (xmlReader.IsStartElement("Voxel"))
            {
                // Retrieve x attribute and store as int
                int x = int.Parse(xmlReader["x"]);
                // Retrieve y attribute and store as int
                int y = int.Parse(xmlReader["y"]);
                // Retrieve z attribute and store as int
                int z = int.Parse(xmlReader["z"]);
                xmlReader.Read();
                int value = int.Parse(xmlReader.Value);
                voxelArray[x, y, z] = value;
            }

            if (xmlReader.IsStartElement("Position"))
            {
                float posX = float.Parse(xmlReader["x"]);
                float posY = float.Parse(xmlReader["y"]);
                float posZ = float.Parse(xmlReader["z"]);
                xmlReader.Read();
                Vector3 loadedPosition = new Vector3(posX, posY, posZ);

                GameObject player = GameObject.Find("Player");
                player.transform.position = loadedPosition;
            }

            if (xmlReader.IsStartElement("Rotation"))
            {
                float rotX = float.Parse(xmlReader["x"]);
                float rotY = float.Parse(xmlReader["y"]);
                float rotZ = float.Parse(xmlReader["z"]);
                xmlReader.Read();
                Vector3 loadedRotation = new Vector3(rotX, rotY, rotZ);

                GameObject player = GameObject.Find("Player");
                player.transform.eulerAngles = loadedRotation;
            }
        }
        return voxelArray;
    }
}