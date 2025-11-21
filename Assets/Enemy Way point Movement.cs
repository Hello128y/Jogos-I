using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypointMovement : MonoBehaviour
{
    [Header("Waypoints")]
    public List<Transform> waypoints; // Assign your waypoint transforms in the inspector

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public bool loop = true;

    private Rigidbody2D rb;
    public Transform visual;
    private int currentWaypointIndex = 0;
    private Vector2 movementDirection;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Validate that we have waypoints
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to the enemy!");
            enabled = false;
            return;
        }

        // Start moving towards the first waypoint
        SetTargetWaypoint(currentWaypointIndex);
    }
 void Update()
    {
         if (rb.linearVelocity.x > 0.01f)
        {
            visual.localScale = new Vector3(7, 7, 7);
        }
        else if(rb.linearVelocity.x < -0.01f)
        {
            visual.localScale = new Vector3(-7, 7, 7);
        }
        else
        {
            visual.localScale = new Vector3(-7, 7, 7);
        }
    }
    void FixedUpdate()
    {
        MoveTowardsWaypoint();
        CheckIfWaypointReached();
    }

    void SetTargetWaypoint(int index)
    {
        if (waypoints.Count == 0) return;

        currentWaypointIndex = index;
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        movementDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Count == 0) return;

        // Update direction every frame for better path correction
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        movementDirection = (targetPosition - (Vector2)transform.position).normalized;

        // Set linear velocity towards the current waypoint
        rb.linearVelocity = movementDirection * moveSpeed;
    }

    void CheckIfWaypointReached()
    {
        if (waypoints.Count == 0) return;

        float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position);

        if (distanceToWaypoint <= waypointReachedDistance)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        // Remove the stop for smoother movement
        // rb.linearVelocity = Vector2.zero;

        // Move to next waypoint
        currentWaypointIndex++;

        // Handle reaching the end of waypoints
        if (currentWaypointIndex >= waypoints.Count)
        {
            if (loop)
            {
                currentWaypointIndex = 0;
            }
            else
            {
                // Stop moving if not looping
                enabled = false;
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        // Set new target waypoint
        SetTargetWaypoint(currentWaypointIndex);
    }
}
