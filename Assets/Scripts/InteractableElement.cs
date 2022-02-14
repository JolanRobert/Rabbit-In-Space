using UnityEngine;

public abstract class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] protected GameObject interactPanel;
    protected ElementType elementType;
<<<<<<< HEAD

    public abstract void Interact();
=======
    
    public void Interact() {
        if (elementType == ElementType.STATION) UIManager.Instance.OpenMinigame(interactPanel);
        else if (elementType == ElementType.PARCEL) UIManager.Instance.OpenPanel(interactPanel);
        else if (elementType == ElementType.FRIDGE) UIManager.Instance.OpenFridge();
        else if (elementType == ElementType.WORKPLAN) UIManager.Instance.OpenWorkplan();
        
    }
>>>>>>> Service

    protected enum ElementType {
        STATION, PARCEL, FRIDGE, WORKPLAN
    }
}


