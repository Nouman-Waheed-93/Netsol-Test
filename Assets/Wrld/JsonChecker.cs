using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonChecker : MonoBehaviour
{
    public string res;
    // Start is called before the first frame update
    void Start()
    {

        string jsonString = "{\"POIs\":" + res + "}";
        Debug.Log(jsonString);
        LatLonArrayContainer llD = JsonUtility.FromJson<LatLonArrayContainer>(jsonString);
        foreach(LatLongDegrees deg in llD.POIs)
        {
            Debug.Log(deg.lon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
