using UnityEngine;

public class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] private GameObject interactPanel;
    protected ElementType elementType;
    
    public virtual void Interact() {
        UIManager.instance.OpenPanel(interactPanel);
    }

    protected enum ElementType {
        STATION, PARCEL
    }
}
