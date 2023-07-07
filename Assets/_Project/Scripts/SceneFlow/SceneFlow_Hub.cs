//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.SceneManagement;

public class SceneFlow_Hub : SceneFlow
{
    //References
    [SerializeField][BoxGroup("References")]
    private FirstPersonController _firstPersonController;

    [SerializeField][BoxGroup("References")]
    private HubStars _stars;
    
    [SerializeField][BoxGroup("References")]
    private SceneLoader[] _scenarios;

    //Game flow variables
    [ReadOnly][BoxGroup("Runtime variables")]
    private bool _playerInteractedWithIris = false;
    
    [ReadOnly][BoxGroup("Runtime variables")]
    private HubSymbol _activatedSymbol = null;
    
    [ReadOnly][BoxGroup("Runtime variables")]
    private bool _loadNextScenario = false;

    //Modules
    private LookAround _lookAroundModule;
    private LockViewOnTarget _lockViewOnTargetModule;

    protected override IEnumerator<float> _SceneFlow()
    {
        //get all necessary modules
        _lookAroundModule = _firstPersonController.GetModule<LookAround>();
        _lockViewOnTargetModule = _firstPersonController.GetModule<LockViewOnTarget>();
        
        //initialize the scene to restore all previously activated stars and bridges
        InitializeScene();

        //wait until the player looked at the iris
        yield return Timing.WaitUntilTrue(() => _playerInteractedWithIris);

        //then disable LookAround
        _lookAroundModule.SetEnabled(false);

        //enable lockViewOnTarget and zoom in (cache previous zoom value)
        _lockViewOnTargetModule.SetEnabled(true);
        float defaultFieldOfView = Camera.main.fieldOfView;
        
        //show stars
        _stars.Show();

        //wait until the player interacted with one of the stars, meaning they activated a symbol
        yield return Timing.WaitUntilFalse(() => _activatedSymbol == null);
        
        //disable all other stars
        _stars.DisableInteractionOnAllStars();
        _stars.HideAllStarsExceptOne(_activatedSymbol);
        
        //remember the activated symbol in the global game data
        GameData.Singleton.activatedSymbols.Add(_activatedSymbol);

        //activate the according bridge (show fog and symbol)
        Bridge[] bridges = FindObjectsOfType<Bridge>(); //jaja, FindObjects is baaaaaaad. But it's quick and dirty and gets the job quickly done here...
        foreach (Bridge bridge in bridges)
        {
            if (bridge.Symbol != _activatedSymbol) continue;
            
            bridge.Activate();
            break;
        }

        //zoom out
        yield return Timing.WaitUntilDone(_lockViewOnTargetModule._ChangeFieldOfView(defaultFieldOfView));

        //player gains control over camera movement again (activate LookAround)
        _lockViewOnTargetModule.SetEnabled(false);
        _lookAroundModule.SetEnabled(true);

        //load next scenario as soon as the player interacted with the bridge symbol
        yield return Timing.WaitUntilTrue(() => _loadNextScenario);
        _scenarios[GameData.Singleton.nextScenarioIndex].Load();
        GameData.Singleton.nextScenarioIndex++;
    }

    private void InitializeScene()
    {
        //load previously activated stars and bridges
        foreach (HubSymbol symbol in GameData.Singleton.activatedSymbols)
        {
            //star
            _stars.LoadStarState(symbol);
            
            //bridge
            Bridge[] bridges = FindObjectsOfType<Bridge>();
            foreach (Bridge bridge in bridges)
            {
                if (bridge.Symbol != symbol) continue;
            
                bridge.LoadState();
                break;
            }
        }
    }

    public void SetPlayerInteractedWithIris(bool interacted) => _playerInteractedWithIris = interacted;
    public void SetActivatedSymbol(HubSymbol symbol) => _activatedSymbol = symbol;
    public void LoadNextScenario() => _loadNextScenario = true;
}