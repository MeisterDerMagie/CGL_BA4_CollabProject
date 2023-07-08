using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;
using Wichtel;

public class AudioManager : SingletonBehaviourDontDestroyOnLoad<AudioManager>
{
    public Sound[] sounds = new Sound[0];

    private void Awake() => InitSingleton();

    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.initialVolume = sound.volume;

            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        //Find sound via name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning($"Sound \"{name}\" not found!");
            return;
        }

        s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)]; //Choose a random clip from the clip array
        s.source.Play(); //Play sound
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

        s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)]; //Choose a random clip from the clip array
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

        s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)]; //Choose a random clip from the clip array
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
