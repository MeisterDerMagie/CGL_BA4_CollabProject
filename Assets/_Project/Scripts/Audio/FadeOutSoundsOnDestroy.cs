//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSoundsOnDestroy : MonoBehaviour
{
    public string[] soundNames;

    private void OnDestroy() => FadeOut();

    private void FadeOut()
    {
        foreach (string sound in soundNames)
        {
            AudioManager.instance.FadeOut(sound);
        }
    }
}