using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    
    private PlayerManager playerManager;
    private NavMeshAgent agent;

    [Header("Parameters")] 
    [SerializeField] [Range(1,10)] private float rabbitSpeed;
    [SerializeField] [Range(1,1000)] private float rabbitAngularSpeed;
    [SerializeField] [Range(1,100)] private float rabbitAcceleration;
    
    void Start() {
        playerManager = PlayerManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        
        agent.speed = rabbitSpeed;
        agent.angularSpeed = rabbitAngularSpeed;
        agent.acceleration = rabbitAcceleration;
    }

    void Update() {
        PlayerManager.Instance.GetAnimation().Speed(agent.velocity.magnitude);
    }

    public void Move(Vector3 newPosition) {
        Debug.Log(newPosition);
        Debug.Log(transform.position);
        if (playerManager.GetInteract().isInteracting) return;
        if (playerManager.GetAnimation().isLock) return;
        if (newPosition == Vector3.negativeInfinity) return;
        newPosition.y = transform.position.y;
        agent.destination = newPosition;

        playerManager.GetInteract().StopInteract();
    }

    public void StopMove() {
        agent.destination = transform.position;
    }

    public void Teleport(Vector3 newPosition, Quaternion newRotation) {
        agent.Warp(newPosition);
        transform.rotation = newRotation;
    }
}
