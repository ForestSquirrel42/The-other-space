using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PingPongScale : MonoBehaviour 
{
    [SerializeField] float scaleSizeToBig;
    [SerializeField] float scaleSizeToSmall;
    [SerializeField] float timeToScale;
    [SerializeField] float lerpInaccuracyCorrection;

    private static PingPongScale instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(MakeScaleBigger());
    }

    private IEnumerator MakeScaleBigger()
    {
        while(true)
        {
            transform.DOScale(scaleSizeToBig, timeToScale);
            yield return new WaitForFixedUpdate();

            if(transform.localScale.x >= scaleSizeToBig - lerpInaccuracyCorrection)
            {
                StartCoroutine(MakeScaleSmaller());
                yield break;
            }
        }
    }

    private IEnumerator MakeScaleSmaller()
    {
        while(true)
        {
            transform.DOScale(scaleSizeToSmall, timeToScale);
            yield return new WaitForFixedUpdate();

            if(transform.localScale.x <= scaleSizeToSmall + lerpInaccuracyCorrection)
            {
                StartCoroutine(MakeScaleBigger());
                yield break;
            }
        }
    }

    public static void StopTweening()
    {
        instance.StopAllCoroutines();
    }
}
