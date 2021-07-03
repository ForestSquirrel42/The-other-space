using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayerMovement : MonoBehaviour // Этот скрипт нужен только для управления кораблем в редакторе, ну или для WebGL возможно сгодится
{
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [SerializeField] float padding = 1.1f;
    [SerializeField] float moveSpeed = 10f;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
        transform.rotation = Quaternion.Euler(0, 0, 0); 
    }
}
