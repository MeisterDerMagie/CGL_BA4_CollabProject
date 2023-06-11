//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void EnableModule<T>() where T : FirstPersonModule
    {
        int enabledModulesCount = 0;
        
        //disable incompatible modules
        string disabledModulesReport = DisableIncompatibleModules<T>();

        //enable module
        foreach (FirstPersonModule module in modules)
        {
            if (module is not T)
                continue;
            
            module.SetEnabled(true);
            enabledModulesCount += 1;
        }

        //show report in console
        if(enabledModulesCount == 0)
            Debug.LogWarning($"Could not enable firstPersonModule of type {typeof(T)}. Is this component missing on the first person controller?");
        //else
        //    Debug.Log($"Enabled module: {typeof(T)}");

        if(disabledModulesReport != string.Empty)
            Debug.Log(disabledModulesReport);
    }

    public void DisableModule<T>() where T : FirstPersonModule
    {
        foreach (FirstPersonModule module in modules)
        {
            if (module is not T)
                continue;

            //disable module
            module.SetEnabled(false);
            return;
        }
        
        Debug.LogWarning($"Could not disable firstPersonModule of type {typeof(T)}. Is this component missing on the first person controller?");
    }

    /*
    /// <returns>returns a report of the outcome</returns>
    public string DisableIncompatibleModules<T>() where T : FirstPersonModule
    {
        List<string> disabledModules = new();
        
        foreach (FirstPersonModule module in modules)
        {
            //disable incompatible modules
            if (module is not T)
            {
                if (!module.IsEnabled)
                    continue;

                if (!module.IncompatibleModules.Contains(typeof(T)))
                    continue;

                module.SetEnabled(false);
                disabledModules.Add(module.GetType().Name);
            }
        }

        //from here on is just the report on which modules have been disabled
        string logString = string.Empty;
        
        if (disabledModules.Count > 0)
        {
            logString = $"Disabled {disabledModules.Count.ToString()} incompatible modules:\n";
            foreach (string moduleName in disabledModules)
            {
                logString += $"{moduleName}\n";
            }
        }

        return logString;
    }*/
    
    /// <returns>returns a report of the outcome</returns>
    public string DisableIncompatibleModules<T>(T enabledModule = null) where T : FirstPersonModule
    {
        List<string> disabledModules = new();
        
        foreach (FirstPersonModule module in modules)
        {
            //disable incompatible modules
            bool isSameModuleType = enabledModule == null ? module is T : module.GetType() == enabledModule.GetType();
            if (!isSameModuleType)
            {
                if (!module.IsEnabled)
                    continue;

                Type moduleType = enabledModule == null ? typeof(T) : enabledModule.GetType();
                bool isCompatible = !module.IncompatibleModules.Contains(moduleType);
                
                if (isCompatible)
                    continue;

                module.SetEnabled(false);
                disabledModules.Add(module.GetType().Name);
                
                #if UNITY_EDITOR
                if(!Application.isPlaying) UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(module);
                #endif
            }
        }

        //from here on is just the report on which modules have been disabled
        string logString = string.Empty;
        
        if (disabledModules.Count > 0)
        {
            logString = $"Disabled {disabledModules.Count.ToString()} incompatible modules:\n";
            foreach (string moduleName in disabledModules)
            {
                logString += $"{moduleName}\n";
            }
        }

        return logString;
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        modules.Clear();
        modules = gameObject.GetComponents<FirstPersonModule>().ToList();
    }
    #endif
}