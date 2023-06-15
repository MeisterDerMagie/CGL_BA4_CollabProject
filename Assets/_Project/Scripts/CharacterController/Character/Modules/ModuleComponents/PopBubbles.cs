using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[DisallowMultipleComponent]
public class PopBubbles : FirstPersonModule
{
    public void ExecutePopBubbles(GameObject bubble)
    {
        if (!IsEnabled) return;

        bubble.GetComponent<BubbleBehavior>().Pop();
    }
}
