//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public string soundName;

    public void Play()
    {
        AudioManager.instance.Play(soundName);
    }
}