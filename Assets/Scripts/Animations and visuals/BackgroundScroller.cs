using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    private Vector2 offset;
    Material background;

    void Start()
    {
        background = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    void Update()
    {
        background.mainTextureOffset += offset * Time.deltaTime;
    }
}