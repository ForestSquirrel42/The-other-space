using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsDamage : MonoBehaviour
{
    [SerializeField] float collisionDamage;

    public float GetCollisionDamage()
    {
        return collisionDamage;
    }

    public void SetCollisionDamage(float value)
    {
        this.collisionDamage = value;
    }
}
