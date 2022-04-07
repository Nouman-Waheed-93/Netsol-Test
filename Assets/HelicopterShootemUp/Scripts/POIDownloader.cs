using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class POIDownloader : MonoBehaviour
{
    public delegate void LatLonDelegate(LatLongDegrees[] pois);
    [SerializeField]
    private string authToken;

    public void GetPOIs(string setID, LatLonDelegate POICallback)
    {
        RestClient.Get("https://poi.wrld3d.com/v1.1/poisets/"+setID+"/pois/?token="+authToken).Then(
           res =>
           {
               string jsonString = "{\"POIs\":" + res.Text + "}";
               LatLonArrayContainer container = JsonUtility.FromJson<LatLonArrayContainer>(jsonString);
               POICallback(container.POIs);
           }
           ).Catch(err =>
           {
               Debug.Log("error");
               var error = err as RequestException;
               Debug.Log(error.Response);
           }
           );
    }
}
