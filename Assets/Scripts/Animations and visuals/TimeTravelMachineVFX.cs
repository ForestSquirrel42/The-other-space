using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeTravelMachineVFX : MonoBehaviour
{
    [Header("Particle systems")]
    [SerializeField] ParticleSystem timeMachineCharge;
    [SerializeField] ParticleSystem timeMachineActivated;
    [SerializeField] ParticleSystem timeMachineDischarge;
    [SerializeField] ParticleSystem[] engineFlames;

    [Header("Parameters")]
    [SerializeField] float delayBetweenVisualEffects = 1.52f;

    private static TimeTravelMachineVFX instance;

    private void Awake()
    {
        instance = this;
    }

    public static IEnumerator ActivateTimeMachine()
    {
        instance.timeMachineCharge.Play();
        AudioManager.PlayAudioByName("Time machine charge");
        instance.GetComponent<SpriteRenderer>().DOFade(0, 3f);

        float particleSize = 1;

        while (particleSize > 0)
        {
            particleSize -= Time.deltaTime;

            instance.engineFlames[0].startSize = particleSize;
            instance.engineFlames[1].startSize = particleSize;

            yield return null;
        }

        yield return new WaitForSeconds(instance.delayBetweenVisualEffects);

        instance.timeMachineActivated.Play();
        instance.timeMachineActivated.transform.parent = null;
    }

    public static void DeactivateTimeMachine()
    {
        if(SceneLoader.GetCurrentSceneByNameStatic().Equals("Level 1-1") 
            || SceneLoader.GetCurrentSceneByNameStatic().Equals("Level 1-4")
            || SceneLoader.GetCurrentSceneByNameStatic().Equals("Level 1-8"))
        {
            instance.timeMachineDischarge.Play();
        }
    }
}
