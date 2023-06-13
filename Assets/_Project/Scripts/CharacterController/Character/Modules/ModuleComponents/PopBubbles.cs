using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[DisallowMultipleComponent]
public class PopBubbles : FirstPersonModule
{
    public float backValue, popValue;

    bool popable;

    public void ExecutePopBubbles(float value)
    {
        if (!IsEnabled) return;

        // Z-headposition should be bigger than the value we set -> Head is far away from the screen
        if (value >= backValue)
            popable = true;

        // if Z-headposition is smaller then the distance at which the player can pop bubbles and he is able to pop them
        if (value <= popValue && popable == true)
        {
            GameObject bubble = TobiiAPI.GetFocusedObject(); // Get the object the player is looking at

            if (bubble != null) // if we have different objects that are gaze aware, check for the tag
            {
                bubble.GetComponent<BubbleBehavior>().Pop();
                popable = false;
            }
        }
    }
}
