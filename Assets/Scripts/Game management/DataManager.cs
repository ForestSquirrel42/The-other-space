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
    public static DataManager Instance { get; set; }

    private void Awake()
    {
        SetUpSingleton();
    }

    public static int GetCurrentLevel() { return Instance.currentLevel; }

    public static void SetCurrentLevel(int level) { Instance.currentLevel = level; }

    public static int GetPlasmaCount() { return Instance.currentPlasmaCount; }

    public static void SetPlasma(int value) { Instance.currentPlasmaCount = value; }

    public static void AddPlasma(int plasmaCount) 
    { Instance.currentPlasmaCount += plasmaCount; OnPlasmaAdded(Instance.currentPlasmaCount); }

    public static void RemovePlasma(int plasmaCount) { Instance.currentPlasmaCount -= plasmaCount; }

    public static int GetMaxShieldEnergy() { return Instance.maxShieldEnergy; }

    public static void SetMaxShieldEnergy(int value) { Instance.maxShieldEnergy = value; }

    public static void AddMaxShieldEnergy(int value) { Instance.maxShieldEnergy += value; }

    public static int GetPlayerHealth() { return Instance.maxPlayerHealth; }

    public static void SetPlayerHealth(int value) { Instance.maxPlayerHealth = value; }

    public static void AddPlayerHealth(int value) { Instance.maxPlayerHealth += value; }

    public static float GetPlayerDamage() { return Instance.playerDamage; }

    public static void SetPlayerDamage(float value) { Instance.playerDamage = value; }

    public static void AddPlayerDamage(float value) { Instance.playerDamage += value; }

    private void SetUpSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}