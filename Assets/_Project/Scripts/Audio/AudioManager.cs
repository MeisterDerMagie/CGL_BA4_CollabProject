using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.initialVolume = sound.volume;

            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning($"Sound \"{name}\" not found!");
            return;
        }
        
        s.source.Play();
        s.source.volume = s.initialVolume;
    }

    public void PlayAndFadeIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning($"Sound \"{name}\" not found!");
            return;
        }
        
        s.source.Play();
        s.source.DOFade(s.initialVolume, 2f).From(0f);
    }

    public void FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning($"Sound \"{name}\" not found!");
            return;
        }
        
        s.source.DOFade(0f, 2f);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning($"Sound \"{name}\" not found!");
            return;
        }
        
        s.source.Stop();
    }
}
