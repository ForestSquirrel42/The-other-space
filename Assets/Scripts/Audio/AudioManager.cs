using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private static AudioManager instance;

    private void Awake()
    {
        SetUpSingleton();

        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public static void PlayAudioByIndex(int clipOrder = 0)
    {
        var s = instance.sounds[clipOrder];
        s?.audioSource.Play();
    }

    public static void PlayAudioByName(string name)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);

        s?.audioSource.PlayOneShot(s.clip);
    }

    private void SetUpSingleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
