using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class PopBubbles : FirstPersonModule
{
    public float backValue, popValue;

    public bool BubbleFocusedLongEnough { get; set; }
    bool popable;

    public void ExecutePopBubbles(float value)
    {
        if (!IsEnabled) return;

        // Z-headposition should be bigger than the value we set -> Head is far away from the screen
        if (value >= backValue)
            popable = true;

        // if Z-headposition is smaller then the distance at which the player can pop bubbles and he is able to pop them
        if (value <= popValue && popable == true && BubbleFocusedLongEnough == true)
        {
            GameObject bubble = TobiiAPI.GetFocusedObject(); // Get the object the player is looking at

            if (bubble != null) // if we have different objects that are gaze aware, check for the tag
            {
                Destroy(bubble);
                popable = false;
                BubbleFocusedLongEnough = false;
            }
        }
    }
}
