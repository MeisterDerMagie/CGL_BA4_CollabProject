using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnRails : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport)
        };


    public void StartMovement()
    {
        if (!IsEnabled)
            return;
        
        
    }

    public void StopMovement()
    {
        
    }
}
