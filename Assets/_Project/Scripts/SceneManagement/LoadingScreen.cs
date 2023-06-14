//(c) copyright by Martin M. Klöckener
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using Wichtel.SceneManagement;

public class LoadingScreen : LoadingScreenBase
{
    [SerializeField]
    private CanvasGroup loadingScreenCanvasGroup;

    [SerializeField]
    private AudioListener audioListener;
    
    public override IEnumerator<float> _ShowLoadingScreen()
    {
        audioListener.enabled = false;
        
        var tween = loadingScreenCanvasGroup.DOFade(1f, 1.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        audioListener.enabled = true;
    }

    protected override IEnumerator<float> _HideAndDestroyLoadingScreen()
    {
        audioListener.enabled = false;
        
        var tween = loadingScreenCanvasGroup.DOFade(0f, 2.5f);
        yield return Timing.WaitUntilDone(tween.WaitForCompletion(true));
        
        Destroy(gameObject);
    }
}