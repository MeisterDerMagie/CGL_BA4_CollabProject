//(c) copyright by Martin M. Klöckener
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Teleport : FirstPersonModule
{
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

    #if UNITY_EDITOR
    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }
    #endif
}