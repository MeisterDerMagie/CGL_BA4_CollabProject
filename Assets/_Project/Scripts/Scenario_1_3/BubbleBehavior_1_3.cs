using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BubbleBehavior_1_3 : MonoBehaviour, IBubble
{
    float movementSpeed, fallingSpeed;
    float minAngle, maxAngle, angle;

    Vector3 axis;

    BubbleValues.RotationAxis rotationAxis;

    bool falling;
    [HideInInspector]
    public bool popAll;

    BubbleValues values;

    GazeAware gazeAware;

    private FirstPersonController firstPersonController;

    // Start is called before the first frame update
    void Start()
    {
        //Getting all important values
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        fallingSpeed = values.fallingSpeed;
        minAngle = values.minAngle;
        maxAngle = values.maxAngle;

        //Gaze Aware Component
        gazeAware = GetComponent<GazeAware>();

        //First Person Controller
        firstPersonController = FindObjectOfType<FirstPersonController>();

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
        //Falling down when popped
        if (falling == true)
            transform.localPosition -= transform.up * (fallingSpeed * Time.deltaTime);

        //Rotation
        if (rotationAxis == BubbleValues.RotationAxis.Both) //On both axes
        {
            transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up) * Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.right);
        }
        else //On one axis
        {
            transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, axis);
        }
    }

    public void Pop()
    {
        falling = true;
        AudioManager.Singleton.Play("Bubble_Dropping");
        Destroy(gameObject, 1.5f);

        //Last Round of Bubbles -> Player can pop all of them
        if (popAll == true)
        {
            if (values.CheckForBubbles()) FindObjectOfType<SceneFlow_Scenario_1_3>().SetAllBubblesPopped();
            return;
        }

        firstPersonController.GetModule<PopBubbles>().SetEnabled(false);

        StartCoroutine(SetAllBubblesPopped(1.4f));
    }

    //Get a random axis on which the bubble should rotate
    Vector3 GetRandomAxis()
    {
        int x = Random.Range(1, 2);
        Vector3 axis;
        axis = x == 1 ? Vector3.up : Vector3.right;
        return axis;
    }

    IEnumerator SetAllBubblesPopped(float time)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<SceneFlow_Scenario_1_3>().SetAllBubblesPopped();
    }
}
