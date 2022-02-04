using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour, IInteractable {

    [SerializeField] private StationSO stationSO;
    [SerializeField] private GameObject stationPanel;

    public Vector3 InteractPosition { get; private set; }

    private void Start() {
        InteractPosition = stationSO.interactPosition;
        stationPanel = Instantiate(stationPanel,transform.GetChild(0));
        stationPanel.GetComponent<Image>().color = stationSO.panelColor;
        stationPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(EndInteract);
    }
    
    public void Interact() {
        stationPanel.SetActive(true);
    }

    public void EndInteract() {
        stationPanel.SetActive(false);
        GameManager.instance.playerManager.GetInteract().isInteracting = false;
    }
}
