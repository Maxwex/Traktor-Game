using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager current;
    public AudioMixerGroup Mixer;
    public Sound[] Sounds;
    
    public float interval;
    private float timePassed;
    
    public Dictionary<string,Sound> SoundDict = new Dictionary<string, Sound>();
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.outputAudioMixerGroup = sound.MixerGroup;
            sound.Source.pitch = sound.pitch;
            sound.Source.volume = sound.voleume;
            sound.Source.loop = sound.loop;
            SoundDict.Add(sound.name,sound);
        }
        
    }

    public void Play(string name)
    { 
        Sound s;
        SoundDict.TryGetValue(name, out s);
        s.Source.Play();
    }
    
    public bool isPlaying(string name)
    { 
        Sound s;
        SoundDict.TryGetValue(name, out s);
        return s.Source.isPlaying;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed < interval) return;
        timePassed -= interval;
        if(!isPlaying("Music"))Play("Music");
    }
    
}
