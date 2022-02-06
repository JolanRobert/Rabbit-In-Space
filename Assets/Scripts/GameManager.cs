using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameState gameState;
    private Camera pointedCam;
    [SerializeField] private Camera miniGameCam;

    public PlayerManager playerManager;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        gameState = GameState.KITCHEN;
    }
    public void StartMinigame() {
        miniGameCam.gameObject.SetActive(true);
        pointedCam = miniGameCam;
    }
    public void EndMinigame()
    {
        pointedCam = Camera.main;
        miniGameCam.gameObject.SetActive(false);
    }
}

public enum GameState {
    FRONT_TRUCK, KITCHEN, GARDEN
}
