//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnAwake : MonoBehaviour
{
    public string[] soundNames;

    private void Awake() => Play();

    private void Play()
    {
        foreach (string sound in soundNames)
        {
            AudioManager.instance.Play(sound);
        }
    }
}