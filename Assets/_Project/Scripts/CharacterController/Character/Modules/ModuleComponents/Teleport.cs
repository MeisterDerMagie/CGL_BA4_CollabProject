//(c) copyright by Martin M. Klöckener
using UnityEngine;

public class Teleport : FirstPersonModule
{
    public void ExecuteTeleport(Vector3 targetDestination)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Teleportation is not enabled.");
            return;
        }
        
        //do teleport here
        Debug.Log($"Teleport to {targetDestination.ToString()}");
    }
}