using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilesLogic : MonoBehaviour 
{
    [SerializeField] float weaponDamage = 10f;
    [SerializeField] GameObject explosionVFX;

    public float GetDamage()
    {
        return weaponDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        if(explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
