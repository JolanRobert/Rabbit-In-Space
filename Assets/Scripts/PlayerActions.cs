using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActions : MonoBehaviour
{
    private SphereCollider collider;
    private NavMeshAgent agent;

    [Header("Parameters")] 
    [SerializeField] [Range(0.1f,3)] private float interactableRange;
    [SerializeField] [Range(1,10)] private float rabbitSpeed;
    [SerializeField] [Range(1,1000)] private float rabbitAngularSpeed;
    [SerializeField] [Range(1,100)] private float rabbitAcceleration;

    [HideInInspector] public List<GameObject> interactableElements = new List<GameObject>();

    void Awake() {
        collider = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Start() {
        collider.radius = interactableRange;
        agent.speed = rabbitSpeed;
        agent.angularSpeed = rabbitAngularSpeed;
        agent.acceleration = rabbitAcceleration;
    }

    public void Move(Vector3 newPosition) {
        if (newPosition != Vector3.negativeInfinity) agent.destination = newPosition;
    }

    public void Interact() {
        
    }
}
