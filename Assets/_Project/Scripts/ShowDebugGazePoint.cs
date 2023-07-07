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

    private bool _displayPoint;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //toggle visibility when pressing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _displayPoint = !_displayPoint;

            if (!_displayPoint)
            {
                _gazePlot.gameObject.SetActive(false);
                return;
            }

            else
            {
                _gazePlot.gameObject.SetActive(true);
            }
        }

        if (!_displayPoint) return;
        
        //update position
        bool tobiiIsConnected = TobiiAPI.IsConnected;
        Vector2 gazePointScreenPosition;
        
            //if tobii is connected
        if (tobiiIsConnected)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            gazePointScreenPosition = gazePoint.Screen;
            if(!gazePoint.IsRecent()) _gazePlot.gameObject.SetActive(false);
        }

            //if tobii is not connected, use mouse position instead
        else
        {
            gazePointScreenPosition = Input.mousePosition;
        }
        
        _gazePlot.anchoredPosition = gazePointScreenPosition;
    }
}