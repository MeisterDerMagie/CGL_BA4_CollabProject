using UnityEngine;
using UnityEngine.Audio;

// Sound class with all important variables we need to change
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip[] clips;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public float initialVolume;
    
    [Range(0.1f, 3f)]
    public float pitch = 1f;
}
