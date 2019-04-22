using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScript {

    public string line;
    public string ID;
    public List<ResponseScript> connectedTo;
    public List<string> connectionIDs;

    public ResponseScript()
    {
        connectedTo = new List<ResponseScript>();
        connectionIDs = new List<string>();
    }
}
