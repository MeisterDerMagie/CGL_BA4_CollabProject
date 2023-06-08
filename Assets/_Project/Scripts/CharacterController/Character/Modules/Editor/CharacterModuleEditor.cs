//(c) copyright by Martin M. Klöckener
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FirstPersonModule), true)]
public class CharacterModuleInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var characterModule = target as FirstPersonModule;
        Debug.Log($"isEnabled: {characterModule.IsEnabled.ToString()}");

        if (characterModule.IsEnabled)
        {
            return base.CreateInspectorGUI();
        }

        else
        {
            VisualElement disabledInspector = new();
            disabledInspector.Add(new Label("This module is disabled."));
            return disabledInspector;
        }
    }
}