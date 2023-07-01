using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastUtility
{
    public enum GetComponentIn
    {
        Self,
        Parent,
        Children
    }
    
    /// <returns>returns null if the hit object doesn't contain the requested component</returns>
    public static T ScreenPointRaycast<T>(Camera cam, Vector3 screenPoint, GetComponentIn scope = GetComponentIn.Self, bool includeInactive = true)
    {
        Ray ray = cam.ScreenPointToRay(screenPoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);

        T hitObject = default(T);
        
        if(scope == GetComponentIn.Self)
            hitObject = hitAnObject ? hit.collider.GetComponent<T>() : default(T);
        
        else if(scope == GetComponentIn.Parent)
            hitObject = hitAnObject ? hit.collider.GetComponentInParent<T>(includeInactive) : default(T);
        
        else if(scope == GetComponentIn.Children)
            hitObject = hitAnObject ? hit.collider.GetComponentInChildren<T>(includeInactive) : default(T);
            
        return hitObject;
    }
}
