using UnityEngine;

public abstract class IInteractable : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] protected GameObject interactPanel;

    public abstract void Interact();
}


