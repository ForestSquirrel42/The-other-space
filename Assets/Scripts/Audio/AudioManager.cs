using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance { get; set; }

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
        var s = Instance.sounds[clipOrder];
        s?.audioSource.Play();
    }

    public static void PlayAudioByName(string name)
    {
        Sound s = Array.Find(Instance.sounds, sound => sound.name == name);

        s?.audioSource.PlayOneShot(s.clip);
    }

    private void SetUpSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
