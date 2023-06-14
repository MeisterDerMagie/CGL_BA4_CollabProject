//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "InterSceneDataHolder", menuName = "Eye/InterSceneDataHolder", order = 0)]
public class InterSceneDataHolder : ScriptableObject
{
    [BoxGroup("Scenario 1")]
    [SerializeField]
    public InterSceneData<bool> loadBubbles = new(false);
}