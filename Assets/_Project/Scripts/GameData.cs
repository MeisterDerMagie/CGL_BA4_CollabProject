using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Wichtel;
// ReSharper disable InconsistentNaming

public class GameData : SingletonBehaviourDontDestroyOnLoad<GameData>
{
    private void Awake() => InitSingleton();

    [BoxGroup("Hub")]
    [SerializeField]
    public int nextScenarioIndex = 0;

    [BoxGroup("Hub")]
    [SerializeField]
    public List<HubSymbol> activatedSymbols = new();

}