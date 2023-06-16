using UnityEngine;
using UnityEngine.Audio;

// Sound class with all important variables we need to change
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
}
