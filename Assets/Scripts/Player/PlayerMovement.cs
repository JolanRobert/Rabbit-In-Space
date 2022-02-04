using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    
    private PlayerManager playerManager;
    private NavMeshAgent agent;

    [Header("Parameters")] 
    [SerializeField] [Range(1,10)] private float rabbitSpeed;
    [SerializeField] [Range(1,1000)] private float rabbitAngularSpeed;
    [SerializeField] [Range(1,100)] private float rabbitAcceleration;

    void Awake() {
        playerManager = PlayerManager.instance;
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Start() {
        agent.speed = rabbitSpeed;
        agent.angularSpeed = rabbitAngularSpeed;
        agent.acceleration = rabbitAcceleration;
    }

    public void Move(Vector3 newPosition) {
        if (playerManager.GetInteract().isInteracting) return;
        if (newPosition == Vector3.negativeInfinity) return;
        newPosition.y = transform.position.y;
        agent.destination = newPosition;

        playerManager.GetInteract().StopInteract();
    }
}
