//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEST_RandomNavMeshPoint : MonoBehaviour
{
    public GameObject prefab;
    public int iterations;
    public string sceneName;

    private void Start()
    {
        Initialize();
        Spawn();
    }

    [Button]
    public void Spawn()
    {
        for (int i = 0; i < iterations; i++)
        {
            Instantiate(prefab, RandomNavMeshPoint.GetRandomPointOnNavMesh(), Quaternion.identity, transform);
        }
    }

    [Button]
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    [Button]
    public void Initialize()
    {
        RandomNavMeshPoint.Initialize();
    }
}