using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScript {

    public string line;
    public string ID;
    public List<ResponseScript> connectedTo;

    public ResponseScript()
    {
        connectedTo = new List<ResponseScript>();
    }
}
