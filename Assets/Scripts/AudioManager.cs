using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] audioSources;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundOnce(int i)
    {
        audioSources[i].Play();
    }
}
