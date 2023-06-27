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

    [Button][DisableInEditorMode]
    public void CheckForBubbles()
    {
        if (transform.childCount == 0)
        {
            FindObjectOfType<SceneFlow_Scenario_1_2>().SetAllBubblesPopped(true);
        }
    }
}
