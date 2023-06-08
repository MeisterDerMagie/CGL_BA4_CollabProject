using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstPersonInputManager : MonoBehaviour
{
    public enum InputTypes
    {
        NoInput,
        Controller,
        Tobii
    }

    [FormerlySerializedAs("_characterController")] [SerializeField]
    private FirstPersonController firstPersonController;
    
    private InputType _currentInput;

    private void Start() => SetInputType(InputTypes.NoInput);

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
}
