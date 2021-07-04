using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum States
    {
        straight,
        wavy,
        loop,
        fromSideToSide,
        redFlyingStation,
        greyFlyingStation,
        waypointsFollower,
        asteroid
    }
    [SerializeField] States currentState;

    [Header("Common variables for any enemy type")]
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;

    [Header("Wavy variables")]
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] float shift;
    [SerializeField] float yChange;
    [SerializeField] Vector2 sideToSideMovement;
    private float newX;
    private float newY;

    [Header("WaypointFollower's variables")]
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] int waypointIndex = 0;
    private List<Transform> waypoints;
    
    [Header("Flying station variables")]
    [SerializeField] float rotationSpeed = 1f;

    void Awake()
    {
        GetVariables();
        GetWaypoints();
    }

    private void Start()
    {
        SetUpMovementType();
    }

    private void GetWaypoints()
    {
        if(currentState == States.redFlyingStation || currentState == States.greyFlyingStation || currentState == States.waypointsFollower)
        {
            waypoints = waveConfig.GetWaypoints(0);
        }
    }

    private void GetVariables()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetUpMovementType()
    {
        switch (currentState)
        {
            case States.straight:
                StartCoroutine(MoveStraight());
                break;
            case States.wavy:
                StartCoroutine(MoveWavy());
                break;
            case States.loop:
                StartCoroutine(MoveLooped());
                break;
            case States.fromSideToSide:
                StartCoroutine(MoveFromSideToSide());
                break;
            case States.asteroid:
                MoveAsteroid();
                break;
            case States.redFlyingStation:
                MoveFlyingStation();
                break;
            case States.waypointsFollower:
                StartCoroutine(MoveToWaypoints());
                break;
            case States.greyFlyingStation:
                StartCoroutine(MoveToWaypointsWithPauses());
                break;
            default:
                break;
        }
    }

    private void MoveFlyingStation()
    {
        StartCoroutine(RotateStationContinuously());
        StartCoroutine(MoveToWaypoints());
    }

    private IEnumerator RotateStationContinuously()
    {
        while(true)
        {
            transform.Rotate(0, 0, rotationSpeed);

            yield return new WaitForFixedUpdate();
        }
    }

    private void RotateStation()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private IEnumerator MoveToWaypointsWithPauses()
    {
        while(true)
        {
            if (GunsOfEnemyBosses.IsShooting == false && waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var speed = moveSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

                RotateStation();

                if (transform.position == targetPosition)
                {
                    GunsOfEnemyBosses.IsShooting = true;
                    waypointIndex++;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveToWaypoints()
    {
        while(true)
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var speed = moveSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;

                    if (gameObject.CompareTag("Boss") && waypointIndex >= waypoints.Count - 1)
                        waypointIndex = 10;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveFromSideToSide()
    {
        while(true)
        {
            transform.Translate(sideToSideMovement * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }

    private void MoveAsteroid()
    {
         rb.velocity = new Vector2(0f, -moveSpeed);
    }

    private IEnumerator MoveLooped()
    {
        while(true)
        {
            newX = Mathf.Cos(-Time.time * frequency) * amplitude;
            newY = Mathf.Sin(-Time.time * frequency) * 1;
            Vector2 tempPosition = new Vector2(newX, newY);
            transform.position = tempPosition;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveWavy()
    {
        while(true)
        {
            newY = transform.position.y - yChange;
            newX = Mathf.Sin(Time.time * frequency) * amplitude;
            Vector2 tempPosition = new Vector2(newX, newY);
            transform.position = tempPosition;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveStraight()
    {
        while(true)
        {
            transform.Translate(new Vector3(0, -moveSpeed, 0) * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }
    
    public void StopMoving()
    {
        StopAllCoroutines();
    }
}
