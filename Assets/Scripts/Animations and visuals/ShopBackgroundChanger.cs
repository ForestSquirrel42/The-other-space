using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBackgroundChanger : MonoBehaviour
{
    [SerializeField] Material[] backgrounds;
    [SerializeField] Material material;

    private enum BackgroundType
    {
        blue,
        purple,
        orange
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        SetShopBackground();
    }

    private void SetShopBackground()
    {
        int currentLevel = DataManager.GetCurrentLevel();

        if (currentLevel == 1 || currentLevel == 10)
            material.mainTexture = backgrounds[(int)BackgroundType.blue].mainTexture;

        else if (currentLevel > 1 && currentLevel < 6)
            material.mainTexture = backgrounds[(int)BackgroundType.purple].mainTexture;

        else if (currentLevel > 5 && currentLevel < 10)
            material.mainTexture = backgrounds[(int)BackgroundType.orange].mainTexture;
    }
}
