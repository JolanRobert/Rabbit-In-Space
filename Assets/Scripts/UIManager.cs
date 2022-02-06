using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject currentPanel;

    [Header("Switch Scene")]
    [SerializeField] private SwitchScene switchScene;
    public Button switchSceneButton;
    public TMP_Text switchSceneText { get; private set; }

    void Awake() {
        instance = this;
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
        GameManager.instance.StartMinigame();
    }

    public void ClosePanel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.instance.GetInteract().isInteracting = false;
        GameManager.instance.EndMinigame();
    }
}
