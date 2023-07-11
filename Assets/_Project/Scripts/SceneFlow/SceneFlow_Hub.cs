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

    [SerializeField][BoxGroup("Settings")]
    private float _secondsBeforeEndScene = 20f;

    [SerializeField][BoxGroup("Settings")]
    private float _waitBeforeVoice = 3f;

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

        //Play sound when the player is in the hub for the first or third time
        if (GameData.Singleton.nextScenarioIndex == 0)
        {
            yield return Timing.WaitForSeconds(_waitBeforeVoice);
            AudioManager.Singleton.Play("Qual lingua humana");
        }
        else if (GameData.Singleton.nextScenarioIndex == 2)
        {
            yield return Timing.WaitForSeconds(_waitBeforeVoice);
            AudioManager.Singleton.Play("The mind wants");
        }

        //wait until the player looked at the iris
        yield return Timing.WaitUntilTrue(() => _playerInteractedWithIris);

        //then disable LookAround
        _lookAroundModule.SetEnabled(false);

        //enable lockViewOnTarget and zoom in (cache previous zoom value)
        _lockViewOnTargetModule.SetEnabled(true);
        float defaultFieldOfView = Camera.main.fieldOfView;
        
        //show stars
        _stars.Show();

        //End the game if the next scene to load would be the last scene
        if (GameData.Singleton.nextScenarioIndex == _scenarios.Length - 1)
        {
            yield return Timing.WaitForSeconds(_secondsBeforeEndScene);
            AudioManager.Singleton.Play("Look around");
            _scenarios[GameData.Singleton.nextScenarioIndex].Load();
        }

        //wait until the player interacted with one of the stars, meaning they activated a symbol
        yield return Timing.WaitUntilFalse(() => _activatedSymbol == null);

        //Play sound when activating a star for the first time
        if (GameData.Singleton.nextScenarioIndex == 0)
            AudioManager.Singleton.Play("The little soul");

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