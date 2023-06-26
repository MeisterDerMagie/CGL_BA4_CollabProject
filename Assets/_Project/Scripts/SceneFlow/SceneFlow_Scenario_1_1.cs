//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.SceneManagement;

public class SceneFlow_Scenario_1_1 : SceneFlow
{
    [SerializeField][BoxGroup("References")]
    private SceneLoader _nextScenario;
    
    //Game flow variables
    [ReadOnly][BoxGroup("Runtime variables")]
    private bool _paintingHasBeenActivated = false;

    protected override IEnumerator<float> _SceneFlow()
    {
        InitializeScene();

        yield return Timing.WaitUntilTrue(() => _paintingHasBeenActivated);
        
        _nextScenario.Load();
    }

    private void InitializeScene()
    {
        //do stuff here that needs to be set up at the beginning of the scene
    }

    public void PaintingActivated() => _paintingHasBeenActivated = true;
}