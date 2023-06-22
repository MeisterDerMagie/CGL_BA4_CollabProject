//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField]
    private HubSymbol _symbol;
    public HubSymbol Symbol => _symbol;

    [SerializeField]
    private Transform _visualsOnActiavation;

    [SerializeField]
    private Interactable _interactable;
    
    public void Activate()
    {
        _visualsOnActiavation.gameObject.SetActive(true);
        _interactable.SetEnabled(true);
    }
}