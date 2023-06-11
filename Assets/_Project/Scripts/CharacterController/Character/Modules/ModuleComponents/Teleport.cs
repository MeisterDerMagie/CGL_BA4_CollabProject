//(c) copyright by Martin M. Klöckener
using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class Teleport : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new()
        {
            typeof(MoveOnRailsAnimated),
            typeof(MoveOnRailsPlayerControlled),
            typeof(MoveOnRailsScripted)
        };
    
    [SerializeField] [HideInInspector]
    private CharacterController _characterController;

    public void ExecuteTeleport()
    {
        if (!IsEnabled)
        {
            //Debug.Log("Teleportation is not enabled.");
            return;
        }
        
        //get random point on current NavMesh
        Vector3 targetDestination = RandomNavMeshPoint.GetRandomPointOnNavMesh();

        //disable character controller, teleport and enable it again
        _characterController.enabled = false;
        transform.position = targetDestination;
        _characterController.enabled = true;
    }
}