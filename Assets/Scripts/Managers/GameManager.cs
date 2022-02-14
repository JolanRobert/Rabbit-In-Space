using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    [SerializeField] private GameState gameState;

    public PlayerManager playerManager;

    void Awake() {
        Instance = this;
    }

    void Start() {
        gameState = GameState.GARDEN;
    }
}

public enum GameState {
    FRONT_TRUCK, KITCHEN, GARDEN
}
