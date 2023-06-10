//(c) copyright by Martin M. Klöckener
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class Teleport : FirstPersonModule
{
    [SerializeField]
    private NavMeshSurface teleportArea;

    [SerializeField] [HideInInspector]
    private CharacterController _characterController;

    public void ExecuteTeleport()
    {
        if (!IsEnabled)
        {
            //Debug.Log("Teleportation is not enabled.");
            return;
        }
        
        //do teleport here
        Vector3 targetDestination = GetRandomPointOnTeleportArea();

        _characterController.enabled = false;
        transform.position = targetDestination;
        _characterController.enabled = true;


        Debug.Log($"Teleport to {targetDestination.ToString()}");
    }
    
    

    //https://forum.unity.com/threads/generating-a-random-position-on-navmesh.873364/
    private Vector3 GetRandomPointOnTeleportArea()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
 
        int maxIndices = navMeshData.indices.Length - 3;
 
        // pick the first indice of a random triangle in the nav mesh
        int firstVertexSelected = Random.Range(0, maxIndices);
        int secondVertexSelected = Random.Range(0, maxIndices);
 
        // spawn on verticies
        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
 
        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];
 
        // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z)
        {
            point = GetRandomPointOnTeleportArea(); // re-roll a position - I'm not happy with this recursion it could be better
        }
        else
        {
            // select a random point on it
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
        }
 
        return point;
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }
    #endif
}