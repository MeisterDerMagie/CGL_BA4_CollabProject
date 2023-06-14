using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastUtility
{
    /// <returns>returns null if the hit object doesn't contain the requested component</returns>
    public static T ScreenPointRaycast<T>(Camera cam, Vector3 screenPoint) where T : MonoBehaviour
    {
        Ray ray = cam.ScreenPointToRay(screenPoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);
        T hitObject = hitAnObject ? hit.collider.GetComponent<T>() : null;
        return hitObject;
    }
}
