using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerInteract playerInteract;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
    }

    public PlayerInput GetInput() {
        return playerInput;
    }

    public PlayerMovement GetMovement() {
        return playerMovement;
    }

    public PlayerInteract GetInteract() {
        return playerInteract;
    }
}
