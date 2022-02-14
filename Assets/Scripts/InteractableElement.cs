using UnityEngine;

public abstract class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] protected GameObject interactPanel;
    protected ElementType elementType;

    public abstract void Interact();

    protected enum ElementType {
        STATION, PARCEL, FRIDGE, WORKPLAN
    }
}


