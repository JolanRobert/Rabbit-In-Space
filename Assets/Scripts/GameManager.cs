using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public PlayerManager playerManager;

    void Awake() {
        instance = this;
    }
}
