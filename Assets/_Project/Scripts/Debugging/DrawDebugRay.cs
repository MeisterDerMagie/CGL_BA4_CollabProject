//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDebugRay : MonoBehaviour
{
    public RectTransform prefab;
    public Transform parent;

    private void Start()
    {
        RaycastUtility.OnDrawDebugRayPosition += InstantiateMarker;
    }

    private void InstantiateMarker(Vector2 screenPosition)
    {
        RectTransform go = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        go.anchoredPosition = screenPosition;
    }
}