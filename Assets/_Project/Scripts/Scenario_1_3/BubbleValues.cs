using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BubbleValues : MonoBehaviour
{
    [Range(0, 3)]
    public float minSpeed, maxSpeed;

    // Variables for falling down
    [BoxGroup("Only important for scenario 1_3")]
    public float fallingSpeed;

    [Range(-360, 360)]
    [BoxGroup("Rotation Variables")]
    public float minAngle, maxAngle;

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
    public bool CheckForBubbles()
    {
        if (transform.childCount - 1 == 0) return true;
        else return false;
    }
}
