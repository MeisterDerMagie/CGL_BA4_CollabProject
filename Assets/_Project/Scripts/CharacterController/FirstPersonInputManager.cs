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
        Player, //automatically chooses Tobii if connected, otherwise falls back to the Controller
        Controller, //use only if you want to use the controller even tough a Tobii is connected
        Tobii //use only if you want to use Tobii even tough no Tobii is connected
    }

    [SerializeField][HideInInspector]
    private FirstPersonController firstPersonController;
    
    private InputType _currentInput;

    private void Start()
    {
        //SetInputType(InputTypes.Player);
        SetInputType(InputTypes.NoInput);
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
            case InputTypes.Player:
                if (TobiiAPI.IsConnected)
                    _currentInput = new TobiiInput(firstPersonController);
                else
                    _currentInput = new ControllerInput(firstPersonController);
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

    //these exist because it's not possible to choose SetInputType(...) from a Unity Event in the inspector. Enums are not a supported parameter type.
    public void SetInputType_NoInput() => SetInputType(InputTypes.NoInput);
    public void SetInputType_Player() => SetInputType(InputTypes.Player);
    public void SetInputType_Controller() => SetInputType(InputTypes.Controller);
    public void SetInputType_Tobii() => SetInputType(InputTypes.Tobii);
    //
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        firstPersonController = GetComponent<FirstPersonController>();
    }
    #endif
}
