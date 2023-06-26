//(c) copyright by Martin M. Klöckener
using System;
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

    private void Awake()
    {
        _visualsOnActiavation.gameObject.SetActive(false);
    }

    public void Activate()
    {
        _visualsOnActiavation.gameObject.SetActive(true);
        _interactable.SetEnabled(true);
    }

    public void LoadState()
    {
        //show the activated visuals, but don't give the player the possibility to interact with the bridge again
        _visualsOnActiavation.gameObject.SetActive(true);
        _interactable.SetEnabled(false);
    }
}