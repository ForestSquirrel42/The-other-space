using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldEnergyBar : MonoBehaviour
{
    [SerializeField] float updateSpeed = 5f;
    private Image energyBar;

    private void Awake()
    {
        energyBar = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        energyBar.color = new Color32(0, 247, 255, 255);
    }

    private void Update()
    {
        UpdateShieldBarSmoothly();
    }

    private void UpdateShieldBarSmoothly()
    {
        if (ShieldLogic.CurrentShieldEnergy > 0)
            energyBar.fillAmount = Mathf.Lerp
            (energyBar.fillAmount,
            ShieldLogic.CurrentShieldEnergy / ShieldLogic.MaxShieldEnergy,
            updateSpeed * Time.deltaTime);
        else
            energyBar.fillAmount = 0;
    }
}
