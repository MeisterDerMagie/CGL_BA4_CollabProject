using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BubbleBehavior_1_2 : MonoBehaviour, IBubble
{
    float movementSpeed;
    float maxY, spawnY;
    float minAngle, maxAngle, angle;

    Vector3 axis;

    BubbleValues.RotationAxis rotationAxis;

    BubbleValues values;

    GazeAware gazeAware;

    // Start is called before the first frame update
    void Start()
    {
        //Getting all important values
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        maxY = values.maxYValue;
        spawnY = values.spawnY;
        minAngle = values.minAngle;
        maxAngle = values.maxAngle;

        //Gaze Aware Component
        gazeAware = GetComponent<GazeAware>();

        //Get random angle amount for rotation (angle = speed)
        angle = Random.Range(minAngle, maxAngle);

        //Axis bubble should rotate on depending on enum
        switch (rotationAxis)
        {
            case (BubbleValues.RotationAxis.X):
                axis = Vector3.up;
                break;
            case (BubbleValues.RotationAxis.Y):
                axis = Vector3.right;
                break;
            case (BubbleValues.RotationAxis.Random):
                axis = GetRandomAxis();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        if (rotationAxis == BubbleValues.RotationAxis.Both) //On both axes
        {
            var newRotationBoth = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up) * Quaternion.AngleAxis(angle, Vector3.right);
            transform.rotation = newRotationBoth;
        }
        else //On one axis
        {
            var newRotation = transform.rotation * Quaternion.AngleAxis(angle, axis);
            transform.rotation = newRotation;
        }

        //Movement
        transform.localPosition -= transform.up * movementSpeed * Time.deltaTime;

        //Respawning
        if (transform.localPosition.y <= maxY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, spawnY, transform.localPosition.z);
        }
    }

    public void Pop()
    {
        if (values.CheckForBubbles()) FindObjectOfType<SceneFlow_Scenario_1_2>().SetAllBubblesPopped(true);
        Destroy(gameObject);
    }

    //Get a random axis on which the bubble should rotate
    Vector3 GetRandomAxis()
    {
        int x = Random.Range(1, 2);
        Vector3 axis;
        axis = x == 1 ? Vector3.up : Vector3.right;
        return axis;
    }
}
