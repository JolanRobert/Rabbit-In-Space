using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    private PlayerActions playerActions;

    void Awake() {
        playerActions = GetComponent<PlayerActions>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Station")) playerActions.interactableElements.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Station")) playerActions.interactableElements.Remove(other.gameObject);
    }
}
