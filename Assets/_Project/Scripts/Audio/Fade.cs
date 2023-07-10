using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fade : MonoBehaviour
{
    AudioSource audioSource;

    float initialVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        audioSource.volume = 0;
    }

    public void FadeIn() => audioSource.DOFade(initialVolume, 1.5f).From(0f);

    public void FadeOut() => audioSource.DOFade(0f, 1.5f).From(initialVolume);
}
