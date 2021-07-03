using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ReceiveDamage(Collision2D collision);
}
