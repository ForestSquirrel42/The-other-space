using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponLogic : MonoBehaviour
{
    [SerializeField] float playerWeaponDamage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        Destroy(gameObject);
    }

    public float GetPlayerWeaponDamage()
    {
        return this.playerWeaponDamage;
    }

    public void AddPlayerWeaponDamage(float value)
    {
        this.playerWeaponDamage += value;
    }

    public void SetPlayerWeaponDamage(float value)
    {
        this.playerWeaponDamage = value;
    }
}
