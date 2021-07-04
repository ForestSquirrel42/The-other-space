using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f);
    }
}
