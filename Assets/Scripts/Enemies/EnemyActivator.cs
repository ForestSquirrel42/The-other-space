using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    private enum BehaviourType
    {
        activator,
        deactivator
    }
    [SerializeField] BehaviourType behaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(behaviour)
        {
            case BehaviourType.activator:
                ActivateShooting(collision);
                ActivateHealth(collision);
                break;
            case BehaviourType.deactivator:
                DeactivateShooting(collision);
                break;
        }
    }

    private void ActivateShooting(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.TryGetComponent(out EnemyShooting sh))
        {
            sh.IsShootingAllowed = true;
        }
        else if (collision.gameObject.CompareTag("Enemy of type chaser") && collision.gameObject.TryGetComponent(out EnemyShooting shootin))
        {
            shootin.IsShootingAllowed = true;
        }
    }

    private void ActivateHealth(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyHealth health)
        && (collision.gameObject.CompareTag("Enemy")
        || collision.gameObject.CompareTag("Enemy of type chaser")
        || collision.gameObject.CompareTag("Boss")
        || collision.gameObject.CompareTag("Asteroid")))
        {
            health.DamageAllowed = true;
        }
    }

    private void DeactivateShooting(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.TryGetComponent(out EnemyShooting sh))
        {
            sh.IsShootingAllowed = false;
        }
        else if (collision.gameObject.CompareTag("Enemy of type chaser") && collision.gameObject.TryGetComponent(out EnemyShooting shootin))
        {
            shootin.IsShootingAllowed = false;
        }
    }
}
