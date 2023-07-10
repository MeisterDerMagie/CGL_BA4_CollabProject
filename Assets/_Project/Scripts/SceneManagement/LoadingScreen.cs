//(c) copyright by Martin M. Klöckener
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.Events;
using Wichtel.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadingScreen : LoadingScreenBase
{
    [SerializeField]
    private CanvasGroup loadingScreenCanvasGroup;

    [SerializeField]
    private AudioListener audioListener;

    [SerializeField]
    private UnityEvent _onLoadingScreenHidden;
    
    public override IEnumerator<float> _ShowLoadingScreen()
    {
        #if UNITY_EDITOR
        if (SkipAnimation) yield break;
        #endif
        
        audioListener.enabled = false;

        loadingScreenCanvasGroup.alpha = 0f;
        var tween = loadingScreenCanvasGroup.DOFade(1f, 1.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        audioListener.enabled = true;
    }

    protected override IEnumerator<float> _HideAndDestroyLoadingScreen()
    {
        #if UNITY_EDITOR
        if (SkipAnimation)
        {
            Destroy(gameObject);
            yield break;
        }
        #endif
        
        audioListener.enabled = false;
        
        var tween = loadingScreenCanvasGroup.DOFade(0f, 2.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        _onLoadingScreenHidden.Invoke();
        
        Destroy(gameObject);
    }
    
    //--- Menu Toggle ---
    #region MenuToggle
    #if UNITY_EDITOR
    private const string MenuName = "Eye/Skip Loading Screen Animation";
    private const string SettingName = "SkipLoadingScreenAnimation";

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