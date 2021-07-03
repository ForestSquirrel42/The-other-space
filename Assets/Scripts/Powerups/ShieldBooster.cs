using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBooster : MonoBehaviour, IPlayerBoost
{
    [SerializeField] Animator animator;
    [SerializeField] ShieldLogic shieldLogics;
    
    void Update()
    {
        MoveBoostItem();
    }

    public IEnumerator StartTimer(float countdown = 10f)
    {
        float currCountdownValue = countdown;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            if (currCountdownValue <= 0)
            {
                shieldLogics.SetShieldEnergy(shieldLogics.GetMaxShieldEnergy());
                animator.SetInteger("IsBoosterActive", 0);
            }
        }
    }

    public void MoveBoostItem()
    {
        transform.Translate(new Vector3(0, -0.5f, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateShieldBooster();
        Debug.Log("Collision took place at: " + collision.transform.position);
    }

    private void ActivateShieldBooster()
    {
        shieldLogics.SetShieldEnergy(shieldLogics.GetMaxShieldEnergy() * 10000);
        animator.SetInteger("IsBoosterActive", 1);
        StartCoroutine(StartTimer());
    }

    public void DeactivateBooster()
    {

    }
}
