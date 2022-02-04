using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameState gameState;

    public PlayerManager playerManager;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        gameState = GameState.KITCHEN;
    }
}

public enum GameState {
    FRONT_TRUCK, KITCHEN, GARDEN
}
