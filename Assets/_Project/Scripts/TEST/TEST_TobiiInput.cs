using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TEST_TobiiInput : MonoBehaviour
{
    public enum InputTypes
    {
        Eye,
        HeadMovement,
        HeadRotation
    }

    [SerializeField]
    InputTypes input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (input)
        {
            case (InputTypes.Eye):
                Debug.Log("Eye Position: " + TobiiAPI.GetGazePoint().Screen);
                break;
            case (InputTypes.HeadMovement):
                Debug.Log("Head Position: " + TobiiAPI.GetHeadPose().Position);
                break;
            case (InputTypes.HeadRotation):
                Debug.Log("Head Rotation: " + TobiiAPI.GetHeadPose().Rotation);
                break;
        }
    }
}
