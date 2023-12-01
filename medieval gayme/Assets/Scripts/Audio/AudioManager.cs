using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
using System.Security.Authentication;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public List<AudioSource> clipHolders;

    void Awake() 
    {
        instance = this;
    }

    public void Start() {
        AudioSource[] ch = GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < ch.Length; i++)
        {
            clipHolders.Add(ch[i]);
        }
    }

    public void PlaySFX(SoundClip sc)
    {
        for (int i = 0; i < clipHolders.Count; i++)
        {
            if(clipHolders[i].clip == sc.clip){
                clipHolders[i].volume = sc.volume;
                clipHolders[i].Play();
            }
        }
    }
}
