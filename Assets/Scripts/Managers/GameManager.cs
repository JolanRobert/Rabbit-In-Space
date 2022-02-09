using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameState gameState;

    public PlayerManager playerManager;

    void Awake() {
        Instance = this;
    }

    void Start() {
        gameState = GameState.KITCHEN;
    }
}

public enum GameState {
    FRONT_TRUCK, KITCHEN, GARDEN
}
