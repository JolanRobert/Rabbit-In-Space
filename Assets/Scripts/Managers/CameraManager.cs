using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera minigameCamera;

    void Awake() {
        Instance = this;
    }

    public void EnableCamera(CameraType cameraType) {
        if (cameraType == CameraType.MAIN) mainCamera.enabled = true;
        else if (cameraType == CameraType.MINIGAME) minigameCamera.enabled = true;
    }
    
    public void DisableCamera(CameraType cameraType) {
        if (cameraType == CameraType.MAIN) mainCamera.enabled = false;
        else if (cameraType == CameraType.MINIGAME) minigameCamera.enabled = false;
    }
}

public enum CameraType {
    MAIN, MINIGAME
}
