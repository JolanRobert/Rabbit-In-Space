using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    private PlayerActions playerActions;

    void Awake() {
        playerActions = GetComponent<PlayerActions>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<IInteractable>() != null) playerActions.interactableElements.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<IInteractable>() != null) playerActions.interactableElements.Remove(other.gameObject);
    }
}
