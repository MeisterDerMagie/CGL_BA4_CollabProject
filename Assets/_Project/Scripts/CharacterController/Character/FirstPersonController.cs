//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField][HideInInspector]
    public List<FirstPersonModule> modules = new();

    /// <returns>returns null if the first person controller does not have the requested module</returns>
    public T GetModule<T>() where T :  FirstPersonModule
    {
        foreach (FirstPersonModule module in modules)
        {
            if (module is not T requestedModule) continue;
            return requestedModule;
        }

        Debug.LogWarning($"Could not get firstPersonModule of type {typeof(T)}. Is this component missing on the first person controller?");
        return null;
    }

    
    private void SetModuleEnabled<T>(bool isEnabled) where T :  FirstPersonModule
    {
        foreach (FirstPersonModule module in modules)
        {
            if (module is not T requestedModule) continue;
            module.SetEnabled(isEnabled);
        }
        
        Debug.LogWarning($"Could not set isEnabled on firstPersonModule of type {typeof(T)}. Is this component missing on the first person controller?");
    }
    
    
    private void SetAllModulesEnabled(bool isEnabled)
    {
        foreach (FirstPersonModule module in modules)
        {
            module.SetEnabled(isEnabled);
        }
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        modules.Clear();
        modules = gameObject.GetComponents<FirstPersonModule>().ToList();
    }
    #endif
}