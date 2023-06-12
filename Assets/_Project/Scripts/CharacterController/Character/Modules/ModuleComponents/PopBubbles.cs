using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBubbles : FirstPersonModule
{
    public float backValue, popValue;

    bool popable;

    public void ExecutePopBubbles(float value)
    {
        if (!IsEnabled) return;

        if (value >= backValue)
            popable = true;

        if (value <= popValue && popable == true)
        {
            Debug.Log("Pop Bubble"); // Get the object we are looking at rn, if thats how it should work
            popable = false;
        }
    }
}
