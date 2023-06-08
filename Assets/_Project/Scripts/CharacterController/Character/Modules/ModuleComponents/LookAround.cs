//(c) copyright by Martin M. Klöckener
using UnityEngine;

public class LookAround : FirstPersonModule
{
    public void ExecuteLookAround(Vector2 amount)
    {
        if (!IsEnabled)
        {
            Debug.Log("Look around is not enabled.");
            return;
        }
        
        //move camera here
        Debug.Log($"move camera on x axis for: {amount.x}");
        Debug.Log($"move camera on y axis for: {amount.y}");
    }
}