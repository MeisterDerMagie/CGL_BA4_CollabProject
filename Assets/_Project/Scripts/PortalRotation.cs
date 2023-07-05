using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    [ReadOnly]
    public float rotationSpeed; //in angles per second

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up);
    }
}
