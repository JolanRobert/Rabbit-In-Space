using UnityEngine;

public abstract class InteractableElement : MonoBehaviour {

    public Vector3 interactPosition;
    [SerializeField] protected Transform interactPanel;

    public abstract void Interact();
}


