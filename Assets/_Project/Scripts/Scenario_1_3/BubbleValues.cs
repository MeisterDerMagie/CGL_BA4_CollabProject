using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BubbleValues : MonoBehaviour
{
    public float maxYValue, spawnY;
    [Range(0, 3)]
    public float minSpeed, maxSpeed;

    // Variables for falling down
    public bool fallOnInteract;

    [ShowIf(nameof(fallOnInteract))]
    public float fallingSpeed;

    [Range(-360, 360)]
    [BoxGroup("Rotation Variables")]
    public float minAngle, maxAngle;

    public bool scene_1_2;

    public enum RotationAxis
    {
        X,
        Y,
        Random,
        Both
    }

    [SerializeField]
    [BoxGroup("Rotation Variables")]
    RotationAxis rotationAxis;

    [Button][DisableInEditorMode]
    public void CheckForBubbles()
    {
        Debug.Log(transform.childCount);
        //Invoke("Check", 0.3f);
        if (transform.childCount - 1 == 0)
        {
            if (scene_1_2)
                FindObjectOfType<SceneFlow_Scenario_1_2>().SetAllBubblesPopped(true);
            else
                FindObjectOfType<SceneFlow_Scenario_1_3>().SetAllBubblesPopped(true);
        }
    }
}
