//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TEST_RandomNavMeshPoint : MonoBehaviour
{
    public int sampleCount = 1000;
    public Color color = Color.yellow;
    private List<Vector3> points = new();

    [Button]
    public void Initialize()
    {
        RandomNavMeshPoint.Initialize();
    }
    
    [Button]
    public void Sample()
    {
        points.Clear();
        
        for (int i = 0; i < sampleCount; i++)
        {
            Vector3 randomNavMeshPoint = RandomNavMeshPoint.GetRandomPointOnNavMesh();
            points.Add(randomNavMeshPoint);
        }
    }

    [Button]
    public void Clear()
    {
        points.Clear();
    }


    private void OnDrawGizmos()
    {
        foreach (Vector3 point in points)
        {
            // Draw a yellow sphere at the random points
            Gizmos.color = color;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}