using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    // Reference to the NavMeshAgent component
    private NavMeshAgent navMeshAgent;
    // Reference to the camera
    public Camera fixedCamera;
    // Queue to store the target points
    private Queue<Vector3> targetPoints = new Queue<Vector3>();
    // Timer to track if the NavMeshAgent is stuck
    private float stuckTimer = 0f;
    // Maximum time allowed for reaching a point
    private float maxTimeToReachPoint = 5f;

    void Start()
    {
        // Get the NavMeshAgent component attached to the player
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the point where the cursor is pointing
            Ray ray = fixedCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Add the point hit by the ray to the target points queue
                targetPoints.Enqueue(hit.point);

                // If the NavMeshAgent is not currently moving, set the next destination
                if (!navMeshAgent.hasPath)
                {
                    SetNextDestination();
                }
            }
        }

        // Check if the NavMeshAgent has reached its destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // Set the next destination if available
                SetNextDestination();
                stuckTimer = 0f; // Reset the timer as the point is reached
            }
        }
        else if (navMeshAgent.hasPath)
        {
            // Increment the stuck timer
            stuckTimer += Time.deltaTime;
            // If the NavMeshAgent is stuck for too long, move to the next point
            if (stuckTimer > maxTimeToReachPoint)
            {
                SetNextDestination();
                stuckTimer = 0f; // Reset the timer
            }
        }
    }

    // Set the next destination from the queue
    private void SetNextDestination()
    {
        if (targetPoints.Count > 0)
        {
            Vector3 nextPoint = targetPoints.Dequeue();
            navMeshAgent.SetDestination(nextPoint);
        }
    }

    // Handle collisions with specific tagged objects
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the object that has the specified tag
            Destroy(collision.gameObject);
        }
    }
}
