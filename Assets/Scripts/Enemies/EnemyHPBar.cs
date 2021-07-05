using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour, IHealthBar
{
    [SerializeField] float updateTime = 0.2f;
    [SerializeField] Image enemyHPBar;
    [SerializeField] Image border;
    [SerializeField] EnemyHealth enemyHealth;

    private void OnEnable()
    {
        enemyHealth.OnHealthChanged += HandleHealthChange;
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthChanged -= HandleHealthChange;
    }

    public void HandleHealthChange(float percentage)
    {
        StartCoroutine(UpdateHealthBar(percentage));
    }

    public IEnumerator UpdateHealthBar(float percentage)
    {
        float preChange = enemyHPBar.fillAmount;
        float counter = 0;

        while (counter < updateTime)
        {
            if(enemyHPBar != null)
            {
                counter += Time.deltaTime;
                enemyHPBar.fillAmount = Mathf.Lerp(preChange, percentage, counter / updateTime);
            }
            yield return null;
        }

        enemyHPBar.fillAmount = percentage;
        if (percentage <= 0.001f)
        {
            Destroy(enemyHPBar);
            Destroy(border);
        }
    }
}
