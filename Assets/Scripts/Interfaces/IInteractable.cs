using UnityEngine;

public interface IInteractable {
    
    public Vector3 InteractPosition { get; }

    public void Interact();
    public void EndInteract();
}
