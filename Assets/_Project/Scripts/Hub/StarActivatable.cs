//(c) copyright by Martin M. Klöckener
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class StarActivatable : MonoBehaviour
{
    [SerializeField]
    private HubSymbol _symbol;
    public HubSymbol Symbol => _symbol;
    
    [SerializeField]
    private UnityEvent _onActivateStar; //give the artists the possibility to do some fancy glow or what ever when the player interacted with the star
    
    public void ActivateStar()
    {
        _onActivateStar.Invoke();
        FindObjectOfType<SceneFlow_Hub>().SetActivatedSymbol(_symbol); //baaad code, but quick and dirty...
    }

    public void LoadState()
    {
        //activate the visuals of the star, but disable the interactable because the player can only activate each star once
        _onActivateStar.Invoke();
        GetComponent<Interactable>()?.SetEnabled(false);
    }
}