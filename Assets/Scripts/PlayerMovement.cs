using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Camera cam;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private NavMeshAgent agent;

    [Header("Parameters")] 
    [SerializeField] [Range(1,3)] private float interactibleRange;
    [SerializeField] [Range(1,10)] private float rabbitSpeed;
    [SerializeField] [Range(1,100)] private float rabbitAcceleration;

    private void Start()
    {
        collider.radius = interactibleRange;
        agent.speed = rabbitSpeed;
        agent.acceleration = rabbitAcceleration;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 newDestination = ScreenToRay(Input.GetTouch(0).position);
                agent.destination = newDestination == Vector3.negativeInfinity ? agent.destination : newDestination;
            }
        }
    }

    Vector3 ScreenToRay(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.green);
            return hit.point+Vector3.up;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red);
            return Vector3.negativeInfinity;
        }
    }
}
