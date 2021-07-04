using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private enum HitExplosionType
    {
        smallShip,
        bigShip,
        asteroid
    }
    [SerializeField] HitExplosionType hitExplosionType;

    private enum DeathExplosionType
    {
        regularShip,
        mediumShip,
        smallBoss,
        bigBoss,
        asteroid
    }
    [SerializeField] DeathExplosionType deathExplosion;

    [Header("Health related")]
    [SerializeField] float health = 100;
    private float maxHealth;
    private bool isDeathInitiated;
    public bool DamageAllowed { get; set; }
    public event Action<float> OnHealthChanged = delegate { };

    [Header("Visual and audio effects")]
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject hitWithoutEmbersVFX;
    [SerializeField] GameObject destructionVFX;
    [SerializeField] Transform _deathExplosionPlacement;
    private GameObject hitExplosion;
    
    [Header("Currency related")]
    [SerializeField] int plasmaPerDeath = 10;

    private void Awake()
    {
        maxHealth = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(DamageAllowed)
        {
            InstantiateSmallExplosion(collision);
            ReceiveDamage(collision);
            StartCoroutine(InitateDeath());
        }
    }

    public void ReceiveDamage(Collision2D collision)
    {
        var collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Player weapon"))
            health -= collisionObj.GetComponent<PlayerWeaponLogic>().GetPlayerWeaponDamage();

        else if (collisionObj.CompareTag("Enemy weapon"))
            health -= collisionObj.GetComponent<EnemyWeaponLogic>().GetWeaponDamage();

        else
            health -= collisionObj.GetComponent<CollisionsDamage>().GetCollisionDamage();

        OnHealthChanged(health / maxHealth);
    }

    private void InstantiateSmallExplosion(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player weapon") && collision.gameObject.GetComponent<PlayerWeaponLogic>().GetPlayerWeaponDamage() < health)
        {
            switch (hitExplosionType)
            {
                case HitExplosionType.smallShip:
                    InstantiateSmallerHitExplosion(collision);
                    break;
                case HitExplosionType.bigShip:
                    InstantiateBiggerHitExplosion(collision);
                    break;
            }
        }
    }

    private void InstantiateBiggerHitExplosion(Collision2D collision)
    {
        hitExplosion = Instantiate(hitVFX, collision.transform.position, Quaternion.Euler(0, 0, 0));
        hitExplosion.transform.parent = this.transform;
        Destroy(hitExplosion, 3f);
    }

    private void InstantiateSmallerHitExplosion(Collision2D collision)
    {
        hitExplosion = Instantiate(hitWithoutEmbersVFX, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        hitExplosion.transform.parent = this.transform;
        Destroy(hitExplosion, 3f);
    }

    private IEnumerator InitateDeath()
    {
        if (health <= 0 && !isDeathInitiated)
        {
            isDeathInitiated = true;
            if(gameObject.CompareTag("Boss"))
            {
               yield return BossDeathVFX.PlayBossExplosion();
            }
            PlayDeathVisualEffects();
            PlayExplosionSound();
            AddPlasma();

            if(transform.parent != null)
                Destroy(transform.parent.gameObject); // Для врагов с полосками здоровья

            Destroy(gameObject, 0.1f);
        }
        else
        {
            yield break;
        }
    }

    private void PlayDeathVisualEffects()
    {
        InstantiateDeathExplosion();

        switch (deathExplosion)
        {
            case DeathExplosionType.mediumShip:
                CameraShake.Shake(duration: 0.5f, amount: 0.035f);
                break;

            case DeathExplosionType.smallBoss:
                CameraShake.Shake(duration: 1.3f, amount: 0.07f);
                break;

            case DeathExplosionType.bigBoss:
                CameraShake.Shake(duration: 4f, amount: 0.1f);
                break;
        }
    }

    private void PlayExplosionSound()
    {
        switch (deathExplosion)
        {
            case DeathExplosionType.regularShip:
                AudioManager.PlayAudioByName("Medium explosion");
                break;
            case DeathExplosionType.mediumShip:
                AudioManager.PlayAudioByName("Medium explosion");
                break;
            case DeathExplosionType.smallBoss:
                AudioManager.PlayAudioByName("Huge explosion");
                break;
            case DeathExplosionType.bigBoss:
                AudioManager.PlayAudioByName("Boss explosion");
                break;
        }
    }

    private void InstantiateDeathExplosion()
    {
        Transform explosionPlacement;
        if (_deathExplosionPlacement != null)
            explosionPlacement = _deathExplosionPlacement;
        else
            explosionPlacement = gameObject.transform;

        var deathExplosion = Instantiate(destructionVFX, explosionPlacement.position, Quaternion.identity);
        deathExplosion.transform.parent = null;
        Destroy(deathExplosion, 3f);
    }

    private void AddPlasma()
    {
        DataManager.AddPlasma(plasmaPerDeath);
    }

    public float GetEnemyHealth()
    {
        return health;
    }
}
