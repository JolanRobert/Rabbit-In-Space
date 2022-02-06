using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera minigameCamera;

    void Awake() {
        instance = this;
    }

    public void EnableMinigameCamera(bool state) {
        minigameCamera.enabled = state;
    }

}
