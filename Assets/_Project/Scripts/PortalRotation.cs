using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    [HideInInspector]
    public float angle;

    // Update is called once per frame
    void Update()
    {
        var newRotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = newRotation;
    }
}
