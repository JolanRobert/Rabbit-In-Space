using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerInteract playerInteract;

    void Awake() {
        instance = this;
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
