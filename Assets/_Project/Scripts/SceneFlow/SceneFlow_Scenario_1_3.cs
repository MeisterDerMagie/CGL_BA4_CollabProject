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

    [SerializeField][BoxGroup("References")]
    private BubbleManager _bubbleManager;

    [SerializeField][BoxGroup("References")]
    private TextManager _textManager;

    [SerializeField][BoxGroup("References")]
    private GameObject _lastBubble;

    //Settings
    [SerializeField][BoxGroup("Settings")]
    private float _riseDuration = 3f;

    [SerializeField][BoxGroup("Settings")]
    private float _waitForLastBubble = 3f;

    //Game flow variables
    [ReadOnly][BoxGroup("Runtime variables")][ShowInInspector]
    private int _totalPoppedBubbles = 0;
    
    [ReadOnly][BoxGroup("Runtime variables")][ShowInInspector]
    private bool _poppedFinalBubble = false;

    private bool _allBubblesPopped, _allBubbleRoundsDone, _lastRoundReached;

    //Modules
    PopBubbles popBubbles;

    protected override IEnumerator<float> _SceneFlow()
    {
        popBubbles = _firstPersonController.GetModule<PopBubbles>();
        InitializeScene();

        //Wait till all bubbles are done
        while (!_lastRoundReached)
        {
            yield return Timing.WaitUntilTrue(() => _allBubblesPopped);
            _textManager.NextQuestion();
            _bubbleManager.NextRound();
            _allBubblesPopped = false;
            popBubbles.SetEnabled(true);
        }
        Debug.Log("Forelast Round Done");

        //Enable Pop Bubbles Module
        popBubbles.SetEnabled(true);
        _allBubblesPopped = false;

        //Function to make player pop all bubbles
        _bubbleManager.AbleToPopAll();
        Debug.Log("Able to pop all bubbles");

        //Wait till all bobbles are popped
        yield return Timing.WaitUntilTrue(() => _allBubblesPopped);
        Debug.Log("all Bubbles Popped");

        //Start "next round" -> will trigger bool _allBubbleRoundsDone to be true
        _bubbleManager.NextRound();
        Debug.Log("Next Round");

        //Wait till all bubbles of last round got popped
        yield return Timing.WaitUntilTrue(() => _allBubbleRoundsDone);
        Debug.Log("Every Round Done");

        //after a while (how long???) a last bubble with the text "I don't choose" moves in front of the player
        yield return Timing.WaitForSeconds(_waitForLastBubble);
        _lastBubble.SetActive(true);

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
        //Everything happens in other scrips (Text and bubble set up)
    }

    [Button][DisableInEditorMode]
    public void IncreasePoppedBubbles() => _totalPoppedBubbles++;
    
    [Button][DisableInEditorMode]
    public void PoppedFinalBubble() => _poppedFinalBubble = true;

    [Button][DisableInEditorMode]
    public void SetAllBubblesPopped() => _allBubblesPopped = true;

    [Button][DisableInEditorMode]
    public void SetAllBubbleRoundsDone() => _allBubbleRoundsDone = true;

    [Button][DisableInEditorMode]
    public void SetLastRoundReached() => _lastRoundReached = true;
}