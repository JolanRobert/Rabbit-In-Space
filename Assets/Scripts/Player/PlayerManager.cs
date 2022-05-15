using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerInteract playerInteract;
    private PlayerAnimation playerAnimation;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
        playerAnimation = GetComponent<PlayerAnimation>();
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

    public PlayerAnimation GetAnimation() {
        return playerAnimation;
    }
}
