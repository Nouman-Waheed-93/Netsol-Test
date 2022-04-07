using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;
using Wrld;

public class HelicopterController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;
    
    private float rotationY = 0;

    private LatLong targetPosition = new LatLong(37.7858, -122.401);

    void Update()
    {
        // Update movement angle from input
        rotationY += Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);

        // Update target position from input
        var latitudeDelta = Mathf.Cos(Mathf.Deg2Rad * rotationY) * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        var longitudeDelta = Mathf.Sin(Mathf.Deg2Rad * rotationY) * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        targetPosition.SetLatitude(targetPosition.GetLatitude() + (latitudeDelta * 0.00006f));
        targetPosition.SetLongitude(targetPosition.GetLongitude() + (longitudeDelta * 0.00006f));

        Api.Instance.SetOriginPoint(targetPosition);
    }
}
