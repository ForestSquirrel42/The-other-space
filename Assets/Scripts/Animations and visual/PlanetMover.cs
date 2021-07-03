using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMover : MonoBehaviour
{
    [SerializeField] float yChange = 0.01f;
    private float newY;

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        newY = transform.position.y - yChange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
