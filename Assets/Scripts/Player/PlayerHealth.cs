using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Player health and shield state")]
    [SerializeField] float playerHealth = 100;
    [SerializeField] float maxPlayerHealth = 100;
    public static event System.Action<float> OnHealthChanged = delegate { };
    private bool isShieldActive;
   
    [Header("Visual effects effects")]
    [SerializeField] GameObject deathExplosionVFX;
    [SerializeField] GameObject hitExplosionParticles;

    private void Start()
    {
        isShieldActive = true;
        maxPlayerHealth = DataManager.GetPlayerHealth();
        playerHealth = maxPlayerHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShieldActive && PlayerMovement.playerHasControl)
        {
            InstantiateHitExplosion(collision);
            ReceiveDamage(collision);
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (playerHealth <= 0)
        {
            AudioManager.PlayAudioByName("Player explosion");
            var explosion = Instantiate(deathExplosionVFX, transform.position, transform.rotation);
            Destroy(explosion, 5f);

            CameraShake.Shake(1f, 0.12f);

            SceneLoader.ActivateDeathScreen();
            Destroy(gameObject, 0.05f);
        }
    }

    public float GetCurrentHealth()
    {
        return playerHealth;
    }
    public float GetMaxHealth()
    {
        return maxPlayerHealth;
    }

    public void ReceiveDamage(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy weapon":
                var damageDealer = collision.gameObject.GetComponent<EnemyProjectilesLogic>();
                playerHealth -= damageDealer.GetComponent<EnemyProjectilesLogic>().GetDamage();
                break;
            case "Enemy":
                var enemyCollisionDamage = collision.gameObject.GetComponent<CollisionsDamage>().GetCollisionDamage();
                playerHealth -= enemyCollisionDamage;
                break;
            case "Asteroid":
                var asteroidCollisionDamage = collision.gameObject.GetComponent<CollisionsDamage>().GetCollisionDamage();
                playerHealth -= asteroidCollisionDamage;
                break;
        }

        OnHealthChanged(playerHealth / maxPlayerHealth);
    }

    private void InstantiateHitExplosion(Collision2D collision)
    {
        Instantiate(hitExplosionParticles, (Vector2)new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 1f), Quaternion.identity);
    }

    public void SetShieldState(bool shieldState)
    {
        this.isShieldActive = shieldState;
    }
}
