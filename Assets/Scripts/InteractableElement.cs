using UnityEngine;

public class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] private GameObject interactPanel;
    protected InteractElementType interactElementType;
    
    public void Interact() {
        if (interactElementType == InteractElementType.STATION) UIManager.Instance.OpenMinigame(interactPanel);
        else if (interactElementType == InteractElementType.PARCEL) UIManager.Instance.OpenPanel(interactPanel);
        
    }

    protected enum InteractElementType {
        STATION, PARCEL
    }
}
