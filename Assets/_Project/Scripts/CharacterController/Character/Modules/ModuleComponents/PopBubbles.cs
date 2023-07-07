using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[DisallowMultipleComponent]
public class PopBubbles : FirstPersonModule
{
    public void ExecutePopBubbles(Vector3 gazePoint)
    {
        if (!IsEnabled) return;

        var bubble = RaycastUtility.ScreenPointRaycast<IBubble>(Camera.main, gazePoint);
        if (bubble == null) return;
        
        Debug.Log("Pop");
        bubble.Pop();
    }
}
