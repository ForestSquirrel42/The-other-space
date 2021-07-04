using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyShootingTypes, IEnemyShootingSounds
{
    public bool IsShootingAllowed { get; set; }

    private void Awake()
    {
        SetUpVariables();
    }

    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        if(IsShootingAllowed)
        {
            StartCoroutine(CountDownAndShoot());
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(StartShooting());
        }
    }

    private void SetUpVariables()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        if (projectileType == ProjectileType.targeted)
            playerPos = References.playerPositionStatic;
    }

    private IEnumerator CountDownAndShoot()
    {
        while (IsShootingAllowed)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                switch (projectileType)
                {
                    case ProjectileType.single:
                        FireNormalType(PlayLaserSFX);
                        break;
                    case ProjectileType.doubled:
                        StartCoroutine(ReleaseTwoProjectiles(PlayLaserSFX));
                        break;
                    case ProjectileType.rotatingBomb:
                        ReleaseRotatingBomb(PlayLaserSFX);
                        break;
                    case ProjectileType.targeted:
                        StartCoroutine(FireTargeted(PlayLaserSFX));
                        break;
                }
                shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
            yield return null;
        }
    }

    public void PlayLaserSFX()
    {
        switch (projectileSound)
        {
            case ProjectileSound.highPitch:
                AudioManager.PlayAudioByName("High pitch laser");
                break;
            case ProjectileSound.middlePitch:
                AudioManager.PlayAudioByName("Middle pitch laser");
                break;
            case ProjectileSound.lowPitch:
                AudioManager.PlayAudioByName("Low pitch laser");
                break;
            case ProjectileSound.middlePitchVariantTwo:
                AudioManager.PlayAudioByName("Middle pitch laser 2");
                break;
        }
    }
}
