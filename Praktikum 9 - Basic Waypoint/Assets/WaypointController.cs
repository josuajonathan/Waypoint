using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;
    
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = rotationSpeed * Time.deltaTime;

        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        //Debug.Log("Distance: " + distance);

        CheckDistanceToWaypoint(distance);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f);
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f);

    }

    void CheckDistanceToWaypoint(float currentDistance)
    {
        if(currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if(targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }
}
