using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthAmbience : MonoBehaviour
{
    [SerializeField]
    private List<string> _soundsToFadeOut = new List<string>();
    
    [SerializeField]
    private List<string> _soundsToFadeIn = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (string sound in _soundsToFadeOut)
        {
            AudioManager.Singleton.FadeOut(sound);
        }

        foreach (string sound in _soundsToFadeIn)
        {
            AudioManager.Singleton.PlayAndFadeIn(sound);
        }

        Destroy(gameObject);
    }
}
