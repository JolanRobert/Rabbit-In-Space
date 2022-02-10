using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    [SerializeField] private GameObject currentPanel;

    [Header("Switch Scene")]
    [SerializeField] private SwitchScene switchScene;
    public Button switchSceneButton;
    public TMP_Text switchSceneText { get; private set; }

    [Header("Garden")]
    [SerializeField] private GameObject menuSeed;

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

    public void CloseMinigame() {
        CameraManager.Instance.DisableCamera(CameraType.MINIGAME);
        ClosePanel();
    }

    public void OpenMenuSeed() {
        menuSeed.transform.DOLocalMoveX(-75, 0.325f).SetEase(Ease.OutBack, 1.87f);
    }

    public void CloseMenuSeed() {
        menuSeed.transform.DOLocalMoveX(75, 0.325f).SetEase(Ease.InBack, 1.87f);
    }
}
