//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.SceneManagement;

public class SceneFlow_Scenario_1_3 : SceneFlow
{
    //References
    [SerializeField][BoxGroup("References")]
    private SceneLoader _nextScenario;
    
    [SerializeField][BoxGroup("References")]
    private FirstPersonController _firstPersonController;

    [SerializeField]
    [BoxGroup("References")]
    private BubbleManager _bubbleManager;
    
    //Settings
    [SerializeField][BoxGroup("Settings")]
    private float _riseDuration = 3f;
    
    //Game flow variables
    [ReadOnly][BoxGroup("Runtime variables")][ShowInInspector]
    private int _totalPoppedBubbles = 0;
    
    [ReadOnly][BoxGroup("Runtime variables")][ShowInInspector]
    private bool _poppedFinalBubble = false;

    private bool _allBubblesPopped;

    protected override IEnumerator<float> _SceneFlow()
    {
        InitializeScene();

        //the player will pop bubbles until they popped a total of 6
        yield return Timing.WaitUntilTrue(() => _allBubblesPopped);
        
        //after they popped 6 bubbles, the player can pop all bubbles but with no effect upon popping them
        
        //after a while (how long???) a last bubble with the text "I don't choose" appears in front of the player
        
        //when the player popped the last bubble, the player rises upwards ...
        yield return Timing.WaitUntilTrue(() => _poppedFinalBubble);
        var moveOnRailsAnimatedModule = _firstPersonController.GetModule<MoveOnRailsAnimated>();
        moveOnRailsAnimatedModule.SetEnabled(true);
        moveOnRailsAnimatedModule.Play();

        //... player rises for a couple of seconds ...
        yield return Timing.WaitForSeconds(_riseDuration);

        //... and then returns to the hub
        _nextScenario.Load();
    }

    private void InitializeScene()
    {
        //do stuff here that needs to be set up at the beginning of the scene
    }

    [Button][DisableInEditorMode]
    public void IncreasePoppedBubbles() => _totalPoppedBubbles++;
    
    [Button][DisableInEditorMode]
    public void PoppedFinalBubble() => _poppedFinalBubble = true;

    [Button][DisableInEditorMode]
    public void SetAllBubblesPopped(bool popped) => _allBubblesPopped = popped;
}