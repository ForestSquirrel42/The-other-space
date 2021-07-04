using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PingPongColors : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] float duration = 5f;

    [Header("First color RGB")]
    [SerializeField] byte r1;
    [SerializeField] byte g1;
    [SerializeField] byte b1;
    [SerializeField] byte alpha1 = 255;

    [Header("Second color RGB")]
    [SerializeField] byte r2;
    [SerializeField] byte g2;
    [SerializeField] byte b2;
    [SerializeField] byte alpha2 = 255;

    private static PingPongColors instance;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(TweenToFirstColor());
    }

    private IEnumerator TweenToFirstColor()
    {
        spriteRenderer.DOColor(new Color32(r1, g1, b1, alpha1), duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(TweenToSecondColor());
    }

    private IEnumerator TweenToSecondColor()
    {
        spriteRenderer.DOColor(new Color32(r2, g2, b2, alpha2), duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(TweenToFirstColor());
    }

    public static void StopTweening()
    {
        instance.StopAllCoroutines();
        instance.alpha1 = (byte)0;
        instance.alpha2 = (byte)0;
        Destroy(instance.gameObject, 0.5f);
    }
}
