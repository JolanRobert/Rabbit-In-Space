using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    public GameObject currentPanel;

    [Header("Switch Scene")]
    [SerializeField] private SwitchScene switchScene;
    public Button switchSceneButton;
    public TMP_Text switchSceneText { get; private set; }

    void Awake() {
        Instance = this;
        Init();
    }

    private void Init() {
        switchSceneText = switchSceneButton.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void SwitchScene() {
        switchScene.Switch();
    }

    public void OpenPanel(GameObject go) {
        currentPanel = go;
        currentPanel.SetActive(true);
    }

    public void ClosePanel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    public void OpenMinigame(GameObject go) {
        CameraManager.Instance.EnableCamera(CameraType.MINIGAME);
        OpenPanel(go);
    }

    public void OpenFridge(GameObject go)
    {
        OpenPanel(go);
    }

    public void CloseMinigame() {
        CameraManager.Instance.DisableCamera(CameraType.MINIGAME);
        ClosePanel();
    }
}
