using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserBooster : MonoBehaviour, IPlayerBoost
{
    [Header("Powerup configuration")]
    [SerializeField] float countDownValue = 3f;
    [SerializeField] float moveSpeed = 0.5f;

    [Header("VFX")]
    [SerializeField] ParticleSystem activationParticles;

    [Header("Script flow")]
    private PlayerShooting playerShooting;
    private float currentCountdownValue;

    private void Update()
    {
        MoveBoostItem();
    }
    
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        GetPlayerShooting(collision);

        SwitchLaserType();
        PlayActivationEffects();

        DestroyBooster();
        
        yield return new WaitForSeconds(1f); // waiting for animation to play
        StartCoroutine(StartTimer(countDownValue));
    }

    public IEnumerator StartTimer(float countdownValue = 3)
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCountdownValue--;

            if(currentCountdownValue <= 0)
            {
                SwitchBackLaserType();
                Destroy(gameObject);
            }
        }
    }

    public void MoveBoostItem()
    {
        transform.Translate(new Vector3(0, -moveSpeed, 0) * Time.deltaTime);
    }

    public void DestroyBooster()
    {
        PingPongColors.StopTweening();
        PingPongScale.StopTweening();
        Destroy(gameObject.GetComponent<Collider2D>());
    }

    private void GetPlayerShooting(Collider2D collision)
    {
        playerShooting = collision.gameObject.GetComponent<PlayerShooting>();
    }

    private void SwitchLaserType()
    {
        playerShooting.laserType = PlayerShooting.LaserType.doubled;
    }

    private void SwitchBackLaserType()
    {
        playerShooting.laserType = PlayerShooting.LaserType.single;
    }

    private void PlayActivationEffects()
    {
        activationParticles.Play();
        AudioManager.PlayAudioByName("Booster pickup");

        var sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.DOFade(0, 2f);
        }
    }
}
