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
        Debug.Log("User Presence: " + TobiiAPI.GetUserPresence().IsUserPresent());

        switch (input)
        {
            case (InputTypes.Eye):
                if (TobiiAPI.GetGazePoint().IsRecent())
                    Debug.Log("Eye Position: " + TobiiAPI.GetGazePoint().Screen);
                break;
            case (InputTypes.HeadMovement):
                if (TobiiAPI.GetHeadPose().IsRecent())
                    Debug.Log("Head Position: " + TobiiAPI.GetHeadPose().Position);
                break;
            case (InputTypes.HeadRotation):
                if (TobiiAPI.GetHeadPose().IsRecent())
                    Debug.Log("Head Rotation: " + TobiiAPI.GetHeadPose().Rotation);
                break;
        }
    }
}
