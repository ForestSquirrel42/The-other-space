using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Text values")]
    [SerializeField] TextMeshProUGUI plasmaCountText;

    [SerializeField] TextMeshProUGUI shieldCapacityText;
    [SerializeField] TextMeshProUGUI shieldUpgradePriceText;

    [SerializeField] TextMeshProUGUI playerCurrentDamageText;
    [SerializeField] TextMeshProUGUI priceOfDamageUpgradeText;

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI priceOfPlayerHealthUpgradeText;

    [Header("Shop values")]
    [SerializeField] int shieldUpgradeValue = 2;
    [SerializeField] int playerHealthUpgradeValue = 1;
    [SerializeField] float damageUpgradeValue = 0.5f;

    [Header("Prices of upgrades")]
    [SerializeField] int priceOfShieldUpgrade = 150;
    [SerializeField] int priceOfHealthUpgrade = 100;
    [SerializeField] int priceOfDamageUpgrade = 150;

    [Header("Apply upgrade buttons")]
    [SerializeField] Button applyDamageUpgrade;
    [SerializeField] Button applyHealthUpgrade;
    [SerializeField] Button applyShieldUpgrade;

    [Header("Other scripts references")]
    private DataSavingSystem DSS;

    private void Awake()
    {
        SetUpReferences();
        SetButtonsAvailability();
    }

    void Start()
    {
        SetUpShopValues();
    }

    private void SetUpReferences()
    {
        DSS = DataSavingSystem.Instance;
    }

    private void SetButtonsAvailability()
    {
        int currentPlasmaCount = DataManager.GetPlasmaCount();

        if (priceOfDamageUpgrade > currentPlasmaCount)
            applyDamageUpgrade.interactable = false;
        else
            applyDamageUpgrade.interactable = true;


        if (priceOfHealthUpgrade > currentPlasmaCount)
            applyHealthUpgrade.interactable = false;
        else
            applyHealthUpgrade.interactable = true;


        if (priceOfShieldUpgrade > currentPlasmaCount)
            applyShieldUpgrade.interactable = false;
        else
            applyShieldUpgrade.interactable = true;
    }

    private void SetUpShopValues()
    {
        plasmaCountText.text = "plasma: " + DataManager.GetPlasmaCount().ToString();

        shieldCapacityText.text = DataManager.GetMaxShieldEnergy().ToString();
        shieldUpgradePriceText.text = this.priceOfShieldUpgrade.ToString();

        playerCurrentDamageText.text = DataManager.GetPlayerDamage().ToString();
        priceOfDamageUpgradeText.text = this.priceOfDamageUpgrade.ToString();

        playerHealthText.text = DataManager.GetPlayerHealth().ToString();
        priceOfPlayerHealthUpgradeText.text = this.priceOfHealthUpgrade.ToString();
    }

    public void UpgradeShield()
    {
        if(DSS != null) 
        {
            if (DataManager.GetPlasmaCount() >= priceOfShieldUpgrade)
            {
                DataManager.AddMaxShieldEnergy(shieldUpgradeValue);
                DataManager.RemovePlasma(priceOfShieldUpgrade);

                SetButtonsAvailability();
                SetUpShopValues();
                PlayUpgradeSound();
                
                DSS.SaveJsonData();
            }
            else
            {
                Debug.Log("Not enough coins, Milord");
            }
        }
        else
        {
            Debug.Log("References in UpgradeShield() method aren't set, setting them");
            SetUpReferences();
            return;
        }
    }

    public void UpgradePlayerDamage()
    {
        if (DSS != null)
        {
            if (DataManager.GetPlasmaCount() >= priceOfDamageUpgrade)
            {
                DataManager.AddPlayerDamage(damageUpgradeValue);
                DataManager.RemovePlasma(priceOfDamageUpgrade);

                SetUpShopValues();
                SetButtonsAvailability();
                PlayUpgradeSound();

                DSS.SaveJsonData();
            }
            else
            {
                Debug.Log("Not enough coins, Milord");
            }
        }
        else
        {
            Debug.Log("References in UpgradeShield() method aren't set, setting them");
            SetUpReferences();
            return;
        }
    }

    public void UpgradePlayerHealth()
    {
        if (DSS != null)
        {
            if (DataManager.GetPlasmaCount() >= priceOfHealthUpgrade)
            {
                DataManager.AddPlayerHealth(playerHealthUpgradeValue);
                DataManager.RemovePlasma(priceOfHealthUpgrade);

                SetUpShopValues();
                SetButtonsAvailability();
                PlayUpgradeSound();

                DSS.SaveJsonData();
            }
            else
            {
                Debug.Log("Not enough coins, Milord");
            }
        }
        else
        {
            Debug.Log("References in UpgradeShield() method aren't set, setting them");
            SetUpReferences();
            return;
        }
    }

    public void PlayUpgradeSound()
    {
        AudioManager.PlayAudioByName("Menu sound");
    }
}
