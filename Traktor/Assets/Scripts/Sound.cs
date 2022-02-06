using System;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip Clip;
    
    [Range(0f,1f)]
    public float voleume;
    [Range(1,3)]
    public float pitch;

    public bool loop;
    
    public AudioMixerGroup MixerGroup;

    [HideInInspector]
    public AudioSource Source;
}
