using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameCurrencyDisplayer : MonoBehaviour
{
    private TextMeshProUGUI currencyText;

    private void Awake()
    {
        currencyText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        currencyText.text = DataManager.GetPlasmaCount().ToString();
        DataManager.OnPlasmaAdded += UpdateIngameCurrency;
    }

    private void OnDisable()
    {
        DataManager.OnPlasmaAdded -= UpdateIngameCurrency;
    }

    private void UpdateIngameCurrency(int value)
    {
        currencyText.text = value.ToString();
    }
}
