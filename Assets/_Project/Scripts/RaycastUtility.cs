using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel.Utilities;

public static class RaycastUtility
{
    public enum GetComponentIn
    {
        Self,
        Parent,
        Children
    }
    
    /// <returns>returns null if the hit object doesn't contain the requested component</returns>
    public static T ScreenPointRaycast<T>(Camera cam, Vector3 screenPoint, GetComponentIn scope = GetComponentIn.Self, bool includeInactive = true, bool compensateForInaccurateGazePoint = true)
    {
        Ray ray = cam.ScreenPointToRay(screenPoint);
        OnDrawDebugRayPosition?.Invoke(screenPoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);

        T hitObject = default(T);

        //shoot a ray at the given screen point
        if(scope == GetComponentIn.Self)
            hitObject = hitAnObject ? hit.collider.GetComponent<T>() : default(T);
        
        else if(scope == GetComponentIn.Parent)
            hitObject = hitAnObject ? hit.collider.GetComponentInParent<T>(includeInactive) : default(T);
        
        else if(scope == GetComponentIn.Children)
            hitObject = hitAnObject ? hit.collider.GetComponentInChildren<T>(includeInactive) : default(T);
            
        if (hitObject != null) return hitObject;
        

        //if we didn't hit anything, shoot multiple rays until we hit something to compensate for a gaze point that's slightly off
        if(compensateForInaccurateGazePoint)
        {
            hitObject = MultipleScreenPointRaycasts<T>(cam, screenPoint, scope, includeInactive);
        }
        
        return hitObject;
    }

    public static Action<Vector2> OnDrawDebugRayPosition = delegate(Vector2 screenPosition) {  };

    private static T MultipleScreenPointRaycasts<T>(Camera cam, Vector3 screenPoint, GetComponentIn scope = GetComponentIn.Self, bool includeInactive = true)
    {
        float radius = (Screen.height / 10f);
        const int rings = 3;
        const int raysFirstRing = 8;
        const int rayIncreasePerRing = 4;
        
        T hitObject = default(T);
        
        //if the center ray didn't hit anything, shoot the outer rays
        for (int i = 0; i < rings; i++)
        {
            float currentRadius = radius / rings * (i + 1);
            int currentAmount = raysFirstRing + (i * rayIncreasePerRing);
            
            foreach (Vector2 rayScreenPoint in PositionArrangements.ArrangeInCircle(screenPoint, currentAmount, currentRadius))
            {
                Ray ray = cam.ScreenPointToRay(rayScreenPoint);
                OnDrawDebugRayPosition?.Invoke(rayScreenPoint);
                bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);

                if(scope == GetComponentIn.Self)
                    hitObject = hitAnObject ? hit.collider.GetComponent<T>() : default(T);
        
                else if(scope == GetComponentIn.Parent)
                    hitObject = hitAnObject ? hit.collider.GetComponentInParent<T>(includeInactive) : default(T);
        
                else if(scope == GetComponentIn.Children)
                    hitObject = hitAnObject ? hit.collider.GetComponentInChildren<T>(includeInactive) : default(T);

                if (hitObject != null) return hitObject;
            }
        }

        return default(T);
    }
}