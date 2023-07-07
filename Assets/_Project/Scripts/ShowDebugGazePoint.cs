//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowDebugGazePoint : MonoBehaviour
{
    [SerializeField]
    private RectTransform _gazePlot;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        _gazePlot.gameObject.SetActive(Input.GetKey(KeyCode.Space) && gazePoint.IsValid);

        _gazePlot.anchoredPosition = gazePoint.Screen;
    }
}