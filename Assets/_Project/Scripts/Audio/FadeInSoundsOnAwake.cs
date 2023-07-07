//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInSoundsOnAwake : MonoBehaviour
{
    public string[] soundNames;

    private void Awake() => FadeIn();

    private void FadeIn()
    {
        foreach (string sound in soundNames)
        {
            AudioManager.Singleton.PlayAndFadeIn(sound);
        }
    }
}