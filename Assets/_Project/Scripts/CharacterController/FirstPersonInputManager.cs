using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(FirstPersonController))]
public class FirstPersonInputManager : MonoBehaviour
{
    public enum InputTypes
    {
        NoInput,
        Controller,
        Tobii
    }

    [SerializeField][HideInInspector]
    private FirstPersonController firstPersonController;
    
    private InputType _currentInput;

    private void Start()
    {
        if (TobiiAPI.IsConnected)
            SetInputType(InputTypes.Tobii);
        else
            SetInputType(InputTypes.Controller);
    }

    private void Update()
    {
        _currentInput.Tick();
    }

    [Button, DisableInEditorMode]
    public void SetInputType(InputTypes inputType)
    {
        switch (inputType)
        {
            case InputTypes.NoInput:
                _currentInput = new NoInput(firstPersonController);
                break;
            case InputTypes.Controller:
                _currentInput = new ControllerInput(firstPersonController);
                break;
            case InputTypes.Tobii:
                _currentInput = new TobiiInput(firstPersonController);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
        }
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        firstPersonController = GetComponent<FirstPersonController>();
    }
    #endif
}
