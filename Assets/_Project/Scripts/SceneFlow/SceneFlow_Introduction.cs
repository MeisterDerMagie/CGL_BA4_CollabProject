//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using Wichtel.SceneManagement;

public class SceneFlow_Introduction : SceneFlow
{
    //References
    [SerializeField]
    private CanvasGroup _whiteScreen;

    [SerializeField]
    private SceneLoader _hub;
    
    protected override IEnumerator<float> _SceneFlow()
    {
        //play the introduction sound
        AudioManager.instance.Play("Introduction");
        
        //wait until the heart rate goes flat
        yield return Timing.WaitForSeconds(25);
        
        //slowly fade to white
        yield return Timing.WaitUntilDone(_whiteScreen.DOFade(1f, 6f).SetEase(Ease.InSine).WaitForCompletion(true));
        
        //wait another short time
        yield return Timing.WaitForSeconds(2f);
        
        //load hub
        _hub.Load();
    }

    private void Skip()
    {
        Timing.KillCoroutines(Coroutine);
        AudioManager.instance.FadeOut("Introduction");
        _hub.Load();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Skip();
    }
}