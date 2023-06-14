using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUtility
{
    public static T Raycast<T>(Camera cam, Vector3 gazePoint, MonoBehaviour script) where T : MonoBehaviour
    {
        Ray ray = cam.ScreenPointToRay(gazePoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);
        T hitObject = hitAnObject ? hit.collider.GetComponent<T>() : null;
        return hitObject;
    }
}
