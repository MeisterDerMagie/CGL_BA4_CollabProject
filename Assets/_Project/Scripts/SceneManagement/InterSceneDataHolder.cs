//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CrossSceneDataHolder", menuName = "Eye/CrossSceneDataHolder", order = 0)]
public class CrossSceneData : ScriptableObject
{
    [BoxGroup("Hub")]
    [SerializeField]
    public int ActiveBridges = 0;

    [BoxGroup("Scenario 1")]
    [SerializeField]
    public ReadOnceData<bool> LoadBubbles = new(false);
}