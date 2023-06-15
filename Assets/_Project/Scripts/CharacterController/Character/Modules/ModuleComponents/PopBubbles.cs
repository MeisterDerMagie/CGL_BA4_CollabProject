using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[DisallowMultipleComponent]
public class PopBubbles : FirstPersonModule
{
    public void ExecutePopBubbles(GameObject bubble)
    {
        bubble.GetComponent<BubbleBehavior>().Pop();
    }
}
