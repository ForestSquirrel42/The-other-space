using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class GameMusic : MonoBehaviour
{
    [SerializeField] float musicVolume = 0.7f;
    [SerializeField] float beginningOfLevelMusicTweenDuration = 2f;

    private AudioSource audioSource;
    private static GameMusic instance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    void Start()
    {
        audioSource.DOFade(musicVolume, beginningOfLevelMusicTweenDuration);
    }

    public static void FadeMusicOut(float duration = 1.2f)
    {
        instance.audioSource.DOFade(0, duration);
    }

    public static void TurnDownVolume()
    {
        instance.audioSource.DOFade(instance.musicVolume / 2, 1f);
    }

    public static void TurnVolumeBack()
    {
        instance.audioSource.DOFade(instance.musicVolume, 1f);
    }

    public static void PauseMusic()
    {
        instance.audioSource.Pause();
    }

    public static void ResumeMusic()
    {
        instance.audioSource.Play();
    }
}
