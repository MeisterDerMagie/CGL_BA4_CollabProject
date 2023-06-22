using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using Tobii.Gaming;
using Wichtel.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private SceneLoader nextScene;

    [SerializeField] private Transform isConnectedView, isNotConnectedView, skipAnimationHint;
    [SerializeField] private CanvasGroup initializingView, messages;
    
    private void Start()
    {
        //reset view
        initializingView.alpha = 0f;
        messages.alpha = 0f;
        isConnectedView.gameObject.SetActive(false);
        isNotConnectedView.gameObject.SetActive(false);

        //run initialize coroutine
        #if UNITY_EDITOR
        if(SkipAnimation)
            Timing.RunCoroutine(_QuickInitialize());
        else
        #endif
            Timing.RunCoroutine(_InitializeAnimation());
        
        skipAnimationHint.gameObject.SetActive(false);
        #if UNITY_EDITOR
        //show hint to skip animation in the editor
        skipAnimationHint.gameObject.SetActive(true);
        #endif
    }

    private IEnumerator<float> _QuickInitialize()
    {
        initializingView.alpha = 1f;

        //wait for connection
        yield return Timing.WaitForSeconds(0.5f);
        
        //check Tobii connection and show corresponding message
        initializingView.alpha = 0f;
        messages.alpha = 1f;
        bool tobiiIsConnected = TobiiAPI.IsConnected;
        if (tobiiIsConnected)
            isConnectedView.gameObject.SetActive(true);
        else
            isNotConnectedView.gameObject.SetActive(true);

        yield return Timing.WaitForSeconds(1f);
        
        //load next scene
        nextScene.Load();
    }

    private IEnumerator<float> _InitializeAnimation()
    {
        //show white background
        yield return Timing.WaitForSeconds(0.5f);
        
        //fade in init message
        var tween = initializingView.DOFade(1f, 0.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        //wait a momemnt
        yield return Timing.WaitForSeconds(1.5f);

        //check Tobii connection and show corresponding message
        bool tobiiIsConnected = TobiiAPI.IsConnected;
        if (tobiiIsConnected)
            isConnectedView.gameObject.SetActive(true);
        else
            isNotConnectedView.gameObject.SetActive(true);

        //fade out init message
        tween = initializingView.DOFade(0f, 0.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));

        //fade in connection message
        tween = messages.DOFade(1f, 0.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        //show the message for a while
        yield return Timing.WaitForSeconds(2f);
        
        //fade out the message
        tween = messages.DOFade(0f, 1.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        //wait another short time
        yield return Timing.WaitForSeconds(0.25f);

        //load next scene
        nextScene.Load();
    }
    
    //--- Menu Toggle ---
    #region MenuToggle
    #if UNITY_EDITOR
    private const string MenuName = "Eye/Skip Connection Animation";
    private const string SettingName = "SkipConnectionAnimation";

    private static bool SkipAnimation
    {
        get => EditorPrefs.GetBool(SettingName, false);
        set => EditorPrefs.SetBool(SettingName, value);
    }
          
    [MenuItem(MenuName, false, 1)]
    private static void ToggleAction() => SkipAnimation = !SkipAnimation;

    [MenuItem(MenuName, true, 1)]
    private static bool ToggleActionValidate()
    {
        Menu.SetChecked(MenuName, SkipAnimation);
        return true;
    }
    #endif
    #endregion
}
