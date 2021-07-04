using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsOfEnemyBosses : EnemyShootingParameters
{
    private enum TypeOfGun
    {
        straightWithPauses,
        targeted,
        targetedSecondType,
        straight
    }
    [SerializeField] TypeOfGun typeOfGun;

    [Header("Guns of enemy bosses parameters")]
    [SerializeField] ParticleSystem gunChargingParticles;
    [SerializeField] float angularVelocityOfBullets = 100f;
    [SerializeField] float waitingForAnimationToPlay = 3.1f;
    [SerializeField] float delayBetweenNextShooting = 10f;
    [SerializeField] float xAddForceModifier = 0f;
    [SerializeField] float yAddForceModifier = 1f;

    [Header("Bool variables for hardcoded shooting mechanics")]
    private bool delayIsMade;
    private static bool firstGunAllowed;
    private static bool secondGunsAllowed;
    private bool countdownIsStarted;
    public static bool IsShooting { get; set; }

    public static GunsOfEnemyBosses instance;

    private void Awake()
    {
        firstGunAllowed = true;
    }
    
    private void Start()
    {
        switch (typeOfGun) 
        {
            case TypeOfGun.straightWithPauses:
                StartCoroutine(ShootStraightWithPauses());
                break;
            case TypeOfGun.targeted:
                StartCoroutine(StartFiringTargeted());
                break;
            case TypeOfGun.targetedSecondType:
                StartCoroutine(PrepareFiringByCurve());
                break;
            case TypeOfGun.straight:
                StartCoroutine(StartShootingStraight());
                break;
        }
    }
   
    private IEnumerator ShootStraightWithPauses()
    {
        while (true)
        {
            if(IsShooting)
            {
                if(!delayIsMade)
                {
                    yield return new WaitForSeconds(0.5f);
                    delayIsMade = true;
                }
                var projectile = Instantiate(enemyLaser, transform.position, transform.rotation);
                var rigidbodyOfProjectile = projectile.GetComponent<Rigidbody2D>();

                rigidbodyOfProjectile.AddRelativeForce(relativeForce * movementSpeed);
                rigidbodyOfProjectile.angularVelocity = angularVelocityOfBullets;

                PlayLaserSFX();

                StartCoroutine(StartCountdown());
                Destroy(projectile, 4f);
            }
            yield return new WaitForSeconds(shotCounterForGreySpaceStation);
        }
    }

    private IEnumerator StartCountdown()
    {
        if (!countdownIsStarted)
        {
            countdownIsStarted = true;

            yield return new WaitForSeconds(3f);

            delayIsMade = false;
            IsShooting = false;
            countdownIsStarted = false;
        }
        else
            yield break;
    }

    private IEnumerator StartShootingStraight()
    {
        if (gameObject.transform.parent.CompareTag("Flying station"))
        {
            yield return new WaitForSeconds(5f);
        }
        
        while(true)
        {
            var projectile = Instantiate(enemyLaser, transform.position, transform.rotation);
            var rigidbodyOfProjectile = projectile.GetComponent<Rigidbody2D>();

            rigidbodyOfProjectile.AddRelativeForce(relativeForce * movementSpeed);
            rigidbodyOfProjectile.angularVelocity = angularVelocityOfBullets;

            PlayLaserSFX();

            Destroy(projectile, 8f);

            yield return new WaitForSeconds(shotCounterForRedSpaceStation);
        }
    }

    private IEnumerator StartFiringTargeted() // This method is responsible for "main" weapon of last boss
    {
        if (!delayIsMade) // Pause before shooting for dramatical boss appearing
        {
            delayIsMade = true;
            yield return new WaitForSeconds(5f);
            StartCoroutine(StartFiringTargeted());
            yield break;
        }
        else if(!firstGunAllowed) // Hardcoded weapons synchronization via bool variables
        {
            yield return null;
            StartCoroutine(StartFiringTargeted());
            yield break;
        }
        else if(firstGunAllowed)
        {
            secondGunsAllowed = false;

            playerPos = References.playerPositionStatic;

            gunChargingParticles.Play();
            AudioManager.PlayAudioByName("Boss gun charge");
            yield return new WaitForSeconds(waitingForAnimationToPlay);

            for (int i = 0; i < numberOfRepeats; i++)
            {
                Vector3 shootingDirection = playerPos.position - transform.position;

                float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + 90;
                var rota = Quaternion.AngleAxis(angle, Vector3.forward);

                var releaseShot = Instantiate(enemyLaser, transform.position, rota);
                var rb = releaseShot.GetComponent<Rigidbody2D>();

                rb.AddRelativeForce(Vector2.down * floatProjectileSpeed);
                rb.AddForce(new Vector2(Random.Range(-addForceRandomization.x, addForceRandomization.x), 0));

                PlayLaserSFX();

                yield return new WaitForSeconds(timeBetweenBursts);
            }
            secondGunsAllowed = true;

            yield return new WaitForSeconds(delayBetweenNextShooting);
            StartCoroutine(StartFiringTargeted());
        }
    }

    private IEnumerator PrepareFiringByCurve()
    {
        if (!delayIsMade)
        {
            delayIsMade = true;
            yield return new WaitForSeconds(5f);
            StartCoroutine(PrepareFiringByCurve());
            yield break;
        }
        else if (!secondGunsAllowed)
        {
            yield return null;
            StartCoroutine(PrepareFiringByCurve());
            yield break;
        }
        else if(secondGunsAllowed)
        {
            firstGunAllowed = false;
            yield return new WaitForSeconds(delayBetweenNextShooting);

            for (int i = 0; i < numberOfRepeats; i++)
            {
                playerPos = References.playerPositionStatic;
                Vector3 shootingDirection = playerPos.position - transform.position;

                float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + 90;
                var rota = Quaternion.AngleAxis(angle, Vector3.forward);

                StartCoroutine(FireByCurve(shootingDirection, angle, rota));
                yield return new WaitForSeconds(timeBetweenBursts);

            }
            firstGunAllowed = true;
            yield return new WaitForSeconds(delayBetweenNextShooting);
            StartCoroutine(PrepareFiringByCurve());
        }
    }

    private IEnumerator FireByCurve(Vector3 shootingDirection, float angle, Quaternion rota)
    {
        // This method is responsible for side weapons of last boss
        var releaseShot = Instantiate(enemyLaser, transform.position, rota);
        var rb = releaseShot.GetComponent<Rigidbody2D>();

        PlayLaserSFX();

        rb.AddRelativeForce(Vector2.down * floatProjectileSpeed) ;
        rb.AddTorque(rotationSpeed);

        while (true)
        {
            if(releaseShot!= null)
            {
                rb.AddForce(new Vector2(shootingDirection.x + xAddForceModifier, shootingDirection.y / yAddForceModifier));
            }
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void PlayLaserSFX()
    {
        if (projectileSound == ProjectileSound.middlePitch)
            AudioManager.PlayAudioByName("Middle pitch laser");

        else if (projectileSound == ProjectileSound.lowPitch)
            AudioManager.PlayAudioByName("Boss laser");

        else if (projectileSound == ProjectileSound.noSound)
            return;
    }

    public void StopShooting()
    {
        if (gunChargingParticles.isPlaying)
            gunChargingParticles.Stop();

        StopAllCoroutines();
    }
}
