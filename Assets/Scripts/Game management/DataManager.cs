using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DataManager : MonoBehaviour
{
    [Header("Progress values")]
    [SerializeField] int currentPlasmaCount;
    [SerializeField] private int currentLevel = 1;

    [Header("Ship properties")]
    [SerializeField] private int maxShieldEnergy = 200;
    [SerializeField] private int maxPlayerHealth = 100;
    [SerializeField] private float playerDamage = 10f;

    public static event Action<int> OnPlasmaAdded = delegate { };
    private static DataManager instance;

    private void Awake()
    {
        SetUpSingleton();
    }

    public static int GetCurrentLevel() { return instance.currentLevel; }

    public static void SetCurrentLevel(int level) { instance.currentLevel = level; }

    public static int GetPlasmaCount() { return instance.currentPlasmaCount; }

    public static void SetPlasma(int value) { instance.currentPlasmaCount = value; }

    public static void AddPlasma(int plasmaCount) 
    { instance.currentPlasmaCount += plasmaCount; OnPlasmaAdded(instance.currentPlasmaCount); }

    public static void RemovePlasma(int plasmaCount) { instance.currentPlasmaCount -= plasmaCount; }

    public static int GetMaxShieldEnergy() { return instance.maxShieldEnergy; }

    public static void SetMaxShieldEnergy(int value) { instance.maxShieldEnergy = value; }

    public static void AddMaxShieldEnergy(int value) { instance.maxShieldEnergy += value; }

    public static int GetPlayerHealth() { return instance.maxPlayerHealth; }

    public static void SetPlayerHealth(int value) { instance.maxPlayerHealth = value; }

    public static void AddPlayerHealth(int value) { instance.maxPlayerHealth += value; }

    public static float GetPlayerDamage() { return instance.playerDamage; }

    public static void SetPlayerDamage(float value) { instance.playerDamage = value; }

    public static void AddPlayerDamage(float value) { instance.playerDamage += value; }

    private void SetUpSingleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}