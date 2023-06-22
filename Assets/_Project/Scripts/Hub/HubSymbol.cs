//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HubSymbol", menuName = "Eye/HubSymbol", order = 0)]
public class HubSymbol : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite;

    public Sprite Sprite => _sprite;
}