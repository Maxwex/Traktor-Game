using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public RectTransform optionPanel;
    public Button main;
   public Button options; 
   public Button continuw;

   public AudioMixer Mixer;
   
   public void ToMenu()
   {
       GameManager.Instance.ToMenuScene();
   }

   public void Close()
   {
       optionPanel.gameObject.SetActive(!optionPanel.gameObject.activeSelf);
   }


   public void SetVolume(float volume)
   {
       Mixer.SetFloat("volume", volume);
   }
   
   public void SetMusikVolume(float volume)
   {
       Mixer.SetFloat("musikVolume", volume);
   }
   
   public void SetSoundVolume(float volume)
   {
       Mixer.SetFloat("soundVolume", volume);
   }
}
