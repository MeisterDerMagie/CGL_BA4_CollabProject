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

        Debug.Log("Pop");
        bubble.GetComponent<IBubble>().Pop();
    }
}
