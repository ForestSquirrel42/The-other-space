using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class IdleMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private static IdleMusic instance;

    private void Awake()
    {
        SetUpSingleton();
    }
    
    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if(level >= 1 && SceneLoader.GetCurrentSceneByNameStatic() != "Shop")
        {
            Destroy(this.gameObject);
        }
    }

    public static void FadeMusic()
    {
        instance.audioSource.DOFade(0, 1.2f);
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
