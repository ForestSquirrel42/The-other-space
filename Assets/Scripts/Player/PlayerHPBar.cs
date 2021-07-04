using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour, IHealthBar
{
    private Image healthBar;

    private void Awake()
    {
        healthBar = gameObject.GetComponent<Image>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnHealthChanged += HandleHealthChange;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= HandleHealthChange;
    }

    public void HandleHealthChange(float percentage)
    {
        StartCoroutine(UpdateHealthBar(percentage));
    }

    public IEnumerator UpdateHealthBar(float percentage)
    {
        healthBar.fillAmount = percentage;
        yield break;
    }
}
