using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveOnRailsScripted : FirstPersonModule
{
    [SerializeField]
    private SplineContainer rails;
    
    
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport),
            typeof(MoveOnRailsPlayerControlled)
        };


    public void StartMovement()
    {
        if (!IsEnabled)
            return;

        if (rails == null)
        {
            Debug.LogError("Rails can't be null!", this);
            return;
        }
        
        
    }

    public void StopMovement()
    {
        
    }
}
