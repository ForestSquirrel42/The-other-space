using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    private static Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private IEnumerator Start()
    {
        //Unfade screen
        if(SceneLoader.GetCurrentSceneByNameStatic() != "Starting scene" && SceneLoader.GetCurrentSceneByNameStatic() != "Shop")
        {
            image.DOFade(0, 2f);
            yield return new WaitForSeconds(2f);
            image.gameObject.SetActive(false);
        }
        else
        {
            if (image != null)
                image.gameObject.SetActive(false);
        }
    }

    public static void FadeScreen(float delay = 1.2f)
    {
        image.gameObject.SetActive(true);

        image.DOFade(1f, delay);
    }
}
