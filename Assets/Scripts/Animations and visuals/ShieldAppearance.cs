using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAppearance : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.activeSelf)
            StartCoroutine(PlayShieldAnimation());
    }

    private IEnumerator PlayShieldAnimation()
    {
        if (animator.GetInteger("IsBoosterActive") == 0)
        {
            animator.SetBool("ReceivingDamage", true);

            yield return new WaitForSeconds(0.1f);

            animator.SetBool("ReceivingDamage", false);
            animator.Play("ShieldFadeOut");
        }
    }
}
