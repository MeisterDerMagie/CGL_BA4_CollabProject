using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.SceneManagement;

public class BubbleBehavior : MonoBehaviour
{
    float movementSpeed, fallingSpeed;
    float maxY, spawnY;
    float minAngle, maxAngle, angle;

    Vector3 axis;

    BubbleValues.RotationAxis rotationAxis;

    bool falling, scene_1_2;
    [HideInInspector]
    public bool popAll;

    [SerializeField]
    BubbleValues values;

    GazeAware gazeAware;

    // Start is called before the first frame update
    void Start()
    {
        //Values from Parent Object
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        maxY = values.maxYValue;
        spawnY = values.spawnY;
        fallingSpeed = values.fallingSpeed;
        minAngle = values.minAngle;
        maxAngle = values.maxAngle;
        scene_1_2 = values.scene_1_2;

        //Gaze Aware Component
        gazeAware = GetComponent<GazeAware>();

        angle = Random.Range(minAngle, maxAngle);

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
        if (rotationAxis == BubbleValues.RotationAxis.Both)
        {
            var newRotationBoth = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up) * Quaternion.AngleAxis(angle, Vector3.right);
            transform.rotation = newRotationBoth;
        }
        else
        {
            var newRotation = transform.rotation * Quaternion.AngleAxis(angle, axis);
            transform.rotation = newRotation;
        }

        if (scene_1_2 == true)
        {
            transform.localPosition -= transform.up * movementSpeed * Time.deltaTime;

            if (transform.localPosition.y <= maxY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, spawnY, transform.localPosition.z);
            }
        }
        else
        {
            if (falling == true)
                transform.localPosition -= transform.up * fallingSpeed * Time.deltaTime;
        }

        if (gazeAware.HasGazeFocus == true)
            ChangeAppearance(6.5f);
        else
            ChangeAppearance(0);
    }

    public void Pop()
    {
        if (scene_1_2 == true || popAll == true)
        {
            values.CheckForBubbles();
            Destroy(gameObject);
        }
        else
        {
            falling = true;
            Destroy(gameObject, 1.2f);
            FindObjectOfType<SceneFlow_Scenario_1_3>().SetAllBubblesPopped(true);
        }
    }

    public void ChangeAppearance(float value)
    {
        GetComponent<MeshRenderer>().material.SetFloat("Divide Color", value);
        Debug.Log("Changed");
    }

    Vector3 GetRandomAxis()
    {
        int x = Random.Range(1, 2);
        Vector3 axis;
        axis = x == 1 ? Vector3.up : Vector3.right;
        return axis;
    }
}
