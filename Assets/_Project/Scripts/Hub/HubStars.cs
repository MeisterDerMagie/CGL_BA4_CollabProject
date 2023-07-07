//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubStars : MonoBehaviour
{
    private List<Star> _stars = new();
    private List<StarActivatable> _activatableStars = new ();

    private void Awake()
    {
        //disable this object when the scene is loaded. it will be activated later when the player looks at the iris
        gameObject.SetActive(false);
        
        //get all stars
        _stars = GetComponentsInChildren<Star>(true).ToList();
        
        //get all activatable stars
        _activatableStars = GetComponentsInChildren<StarActivatable>(true).ToList();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        foreach (Star star in _stars)
        {
            star.ShowStar();
        }
    }

    public void LoadStarState(HubSymbol symbol)
    {
        foreach (StarActivatable star in _activatableStars)
        {
            if(star.Symbol != symbol) continue;
            
            star.LoadState();
        }
    }

    public void DisableInteractionOnAllStars()
    {
        foreach (StarActivatable star in _activatableStars)
        {
            star.GetComponent<Interactable>().SetEnabled(false);
        }
    }

    //hide all stars except the one with the passed symbol
    public void HideAllStarsExceptOne(HubSymbol symbol)
    {
        foreach (StarActivatable star in _activatableStars)
        {
            if(star.Symbol == symbol) continue;
            
            star.gameObject.SetActive(false);
        }
    }
}