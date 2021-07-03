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

    private bool gettingBigger;
    private bool gettingSmaller;

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
        gettingBigger = true;
       
        while(gettingBigger)
        {
            transform.DOScale(scaleSizeToBig, timeToScale);
            yield return new WaitForFixedUpdate();

            if(transform.localScale.x >= scaleSizeToBig - lerpInaccuracyCorrection)
            {
                gettingBigger = false;
                StartCoroutine(MakeScaleSmaller());
                yield break;
            }
        }
    }

    private IEnumerator MakeScaleSmaller()
    {
        gettingSmaller = true;

        while(gettingSmaller)
        {
            transform.DOScale(scaleSizeToSmall, timeToScale);
            yield return new WaitForFixedUpdate();

            if(transform.localScale.x <= scaleSizeToSmall + lerpInaccuracyCorrection)
            {
                gettingSmaller = false;
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
