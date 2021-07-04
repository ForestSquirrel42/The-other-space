using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private enum TypeOfFollower
    {
        straightLineMover,
        playerChaser,
        boss
    }
    [SerializeField] TypeOfFollower states;

    [Header("Monitoring player position")]
    [SerializeField] Transform playerPos;
    private bool lookingAtPlayer = false;
    
    [Header("References and variables from other scripts")]
    List<Transform> waypoints;
    List<Transform> waypoints2;

    [Header("Movement and rotation variables")]
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] float rotationTowardsPlayerSpeed = 0.1f;
    [SerializeField] float moveSpeed;
    public float xModifier;
    public float yModifier;
    private int waypointIndex = 0;
    private Rigidbody2D rb;
    private Vector3 dir;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetUpReferences();

        if (states == TypeOfFollower.boss)
            StartCoroutine(LookAtPlayer());
    }

    private void Update()
    {
        if (states == TypeOfFollower.straightLineMover)
            MoveOnStraightLine();

        else if (states == TypeOfFollower.playerChaser)
            ChasePlayer();
    }

    private void SetUpReferences()
    {
        playerPos = References.playerPositionStatic;

        if(waveConfig != null)
        {
            waypoints = waveConfig.GetWaypoints(0);
            waypoints2 = waveConfig.GetWaypoints(1);
        }
    }

    private void MoveOnStraightLine()
    {
        // In order to place one ship above another, i've created a copy of the prefab and doing a name check here
        if (gameObject.name == "Turnung ship(Clone)") 
        {
            var waypoints = this.waypoints;
            InitiateMoving(waypoints);
        }
        else if (gameObject.name == "Turnung ship 2(Clone)")
        {
            var waypoints = this.waypoints2;
            InitiateMoving(waypoints);
        }
    }

    private void InitiateMoving(List<Transform> waypoints)
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var vectorTwoTransform = new Vector2(transform.position.x, transform.position.y);
            var targetPosition = new Vector2(waypoints[waypointIndex].transform.position.x, waypoints[waypointIndex].transform.position.y);
            var speed = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), targetPosition, speed);
            if (vectorTwoTransform == targetPosition)
            {
                waypointIndex++;
                StartCoroutine(LookAtPlayer());
                if (waypointIndex >= waypoints.Count)
                {
                    waypointIndex = 0;
                }
            }
        }
    }

    private IEnumerator LookAtPlayer()
    {
        if(!lookingAtPlayer && References.GetPlayerMovementStatic() != null)
        {
            lookingAtPlayer = true;
            while (true)
            {
                var angle = GetPlayerPosition();
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationTowardsPlayerSpeed);
                yield return null;
            }
        }
    }

    private float GetPlayerPosition()
    {
        dir = playerPos.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        return angle;
    }

    private void ChasePlayer()
    {
        StartCoroutine(LookAtPlayer());
        Vector2 direction = new Vector2(dir.x + xModifier, dir.y + yModifier);
        rb.velocity = direction;
    }

    public void StopFollowing()
    {
        StopAllCoroutines();
    }
}
