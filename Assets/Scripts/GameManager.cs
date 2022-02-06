using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameState gameState;

    public PlayerManager playerManager;

    void Awake() {
        instance = this;
    }

    void Start() {
        gameState = GameState.KITCHEN;
    }
    
    public void StartMinigame() {
        CameraManager.instance.EnableMinigameCamera(true);
    }
    
    public void EndMinigame() {
        CameraManager.instance.EnableMinigameCamera(false);
    }
}

public enum GameState {
    FRONT_TRUCK, KITCHEN, GARDEN
}
