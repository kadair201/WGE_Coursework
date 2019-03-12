using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class XMLSerializeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TestDataClass tdc = new TestDataClass("MyName", "This is a serialised item");
            XmlSerializer x = new XmlSerializer(tdc.GetType());
            System.IO.FileStream file = System.IO.File.Create("TestFile.xml");
            x.Serialize(file, tdc);
            file.Close();
        }
    }
}
