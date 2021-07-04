using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour, IDamageable
{
    private static ShieldLogic instance;

    [Header("Shield parameters")]
    [SerializeField] float currentShieldEnergy;
    [SerializeField] float maxShieldEnergy;
    [SerializeField] Transform defaultTransform;

    [Header("References to other scripts")]
    [SerializeField] PlayerHealth playerHealth;

    public static float CurrentShieldEnergy
    {
        get { return instance.currentShieldEnergy; }
    }

    public static float MaxShieldEnergy
    {
        get { return instance.maxShieldEnergy; }
    }

    private void Awake()
    {
        if (playerHealth == null)
            playerHealth = GetComponentInParent<PlayerHealth>();

        instance = this;
    }

    private void OnEnable()
    {
        playerHealth.SetShieldState(true);
    }

    private void Start()
    {
        maxShieldEnergy = DataManager.GetMaxShieldEnergy();
        currentShieldEnergy = maxShieldEnergy;
        StartCoroutine(RegenerateShieldPassively());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReceiveDamage(collision);
        ManageShieldState();
    }

    public void ReceiveDamage(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy weapon":
                var damageDealer = collision.gameObject.GetComponent<EnemyWeaponLogic>();
                currentShieldEnergy -= damageDealer.GetComponent<EnemyWeaponLogic>().GetWeaponDamage();
                break;
            case "Enemy":
                var enemyCollisionDamage = collision.gameObject.GetComponent<CollisionsDamage>().GetCollisionDamage();
                currentShieldEnergy -= enemyCollisionDamage;
                break;
            case "Asteroid":
                var asteroidCollisionDamage = collision.gameObject.GetComponent<CollisionsDamage>().GetCollisionDamage();
                currentShieldEnergy -= asteroidCollisionDamage;
                break;
        }
    }

    private void ManageShieldState()
    {
        if (currentShieldEnergy <= 0)
        {
            playerHealth.SetShieldState(false);
            gameObject.transform.position = new Vector3(50, 0, 0);
            return;
        }
        else if (currentShieldEnergy > 0)
        {
            playerHealth.SetShieldState(true);

            if (gameObject.transform.position != defaultTransform.position)
                gameObject.transform.position = defaultTransform.position;
        }
    }

    private IEnumerator RegenerateShieldPassively()
    {
        while(true)
        {
            if(currentShieldEnergy < maxShieldEnergy)
                currentShieldEnergy += maxShieldEnergy / 100;

            ManageShieldState();
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetShieldEnergy(float shieldEnergy)
    {
        currentShieldEnergy = shieldEnergy;
    }

    public float GetMaxShieldEnergy()
    {
        return maxShieldEnergy;
    }
}

