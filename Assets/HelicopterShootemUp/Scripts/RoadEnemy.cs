using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;

public class RoadEnemy : MonoBehaviour
{
    private TransportPositioner m_transportPositioner;
    
    public void Spawn(LatLong latLong)
    {
        var options = new TransportPositionerOptionsBuilder()
            .SetInputCoordinates(latLong.GetLatitude(), latLong.GetLongitude())
            .Build();

        m_transportPositioner = Api.Instance.TransportApi.CreatePositioner(options);
        m_transportPositioner.OnPointOnGraphChanged += OnPointOnGraphChanged;
    }
    private void OnPointOnGraphChanged()
    {
        if (m_transportPositioner.IsMatched())
        {
            var pointOnGraph = m_transportPositioner.GetPointOnGraph();

            var outputLLA = LatLongAltitude.FromECEF(pointOnGraph.PointOnWay);
            const double verticalOffset = 1.0;
            outputLLA.SetAltitude(outputLLA.GetAltitude() + verticalOffset);
            var outputPosition = Api.Instance.SpacesApi.GeographicToWorldPoint(outputLLA);

            transform.position = outputPosition;
        }
    }

}
