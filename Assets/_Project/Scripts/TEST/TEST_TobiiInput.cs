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

    Vector3 headPos;
    Vector3 highestPos, lowestPos;

    Vector2 gazePoint;
    Vector2 highestGaze, lowestGaze;

    Quaternion headRotation;
    Quaternion highestRotation, lowestRotation;

    // Start is called before the first frame update
    void Start()
    {
        lowestPos = new Vector3(100, 100, 100);
        highestPos = new Vector3(0, 0, -100);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("User Presence: " + TobiiAPI.GetUserPresence().IsUserPresent());

        switch (input)
        {
            case (InputTypes.Eye):
                if (TobiiAPI.GetGazePoint().IsRecent())
                {
                    gazePoint = TobiiAPI.GetGazePoint().Screen;

                    if (gazePoint.x > highestGaze.x)
                        highestGaze.x = gazePoint.x;
                    else if (gazePoint.x < lowestGaze.x)
                        lowestGaze.x = gazePoint.x;

                    if (gazePoint.y > highestGaze.y)
                        highestGaze.y = gazePoint.y;
                    else if (gazePoint.y < lowestGaze.y)
                        lowestGaze.y = gazePoint.y;

                    Debug.Log("Highest Eye Position Values: " + highestGaze);
                    Debug.Log("Lowest Eye Position Values: " + lowestGaze);
                }
                break;
            case (InputTypes.HeadMovement):
                if (TobiiAPI.GetHeadPose().IsRecent())
                {
                    headPos = TobiiAPI.GetHeadPose().Position;

                    if (headPos.x > highestPos.x)
                        highestPos.x = headPos.x;
                    else if (headPos.x < lowestPos.x)
                        lowestPos.x = headPos.x;

                    if (headPos.y > highestPos.y)
                        highestPos.y = headPos.y;
                    else if (headPos.y < lowestPos.y)
                        lowestPos.y = headPos.y;

                    if (headPos.z > highestPos.z)
                        highestPos.z = headPos.z;
                    else if (headPos.z < lowestPos.z)
                        lowestPos.z = headPos.z;

                    Debug.Log("Highest Position Values: " + highestPos);
                    Debug.Log("Lowest Position Values: " + lowestPos);
                }
                break;
            case (InputTypes.HeadRotation):
                if (TobiiAPI.GetHeadPose().IsRecent())
                {
                    headRotation = TobiiAPI.GetHeadPose().Rotation;

                    if (headRotation.x > highestRotation.x)
                        highestRotation.x = headRotation.x;
                    else if (headRotation.x < lowestRotation.x)
                        lowestRotation.x = headRotation.x;

                    if (headRotation.y > highestRotation.y)
                        highestRotation.y = headRotation.y;
                    else if (headRotation.y < lowestRotation.y)
                        lowestRotation.y = headRotation.y;

                    if (headRotation.z > highestRotation.z)
                        highestRotation.z = headRotation.z;
                    else if (headRotation.z < lowestRotation.z)
                        lowestRotation.z = headRotation.z;

                    Debug.Log("Highest Rotation Values: " + highestRotation);
                    Debug.Log("Lowest Rotation Values: " + lowestRotation);
                    //Debug.Log("Euler Angles: " + headRotation.eulerAngles);
                }
                break;
        }
    }
}
