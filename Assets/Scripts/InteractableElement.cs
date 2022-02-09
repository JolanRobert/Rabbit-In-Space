using UnityEngine;

public class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] private GameObject interactPanel;
    protected ElementType elementType;
    
    public void Interact() {
        if (elementType == ElementType.STATION) UIManager.Instance.OpenMinigame(interactPanel);
        else if (elementType == ElementType.PARCEL) UIManager.Instance.OpenPanel(interactPanel);
        else if (elementType == ElementType.FRIDGE) UIManager.Instance.OpenFridge(interactPanel);
        
    }

    protected enum ElementType {
        STATION, PARCEL, FRIDGE
    }
}
