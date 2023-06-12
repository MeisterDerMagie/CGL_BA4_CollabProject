using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(MoveOnRailsAnimated),
            typeof(MoveOnRailsPlayerControlled),
            typeof(MoveOnRailsScripted)
        };
    
    
}
