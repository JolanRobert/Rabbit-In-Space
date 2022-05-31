using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    
    //Interaction
    private Coroutine interactCR;
    private bool isTryingToInteract;
    public bool isInteracting;
    
    public void TryInteract(IInteractable interactableGO) {
        if (isTryingToInteract) StopCoroutine(interactCR);
        interactCR = StartCoroutine(InteractCR(interactableGO));
    }

    private IEnumerator InteractCR(IInteractable interactableGO) {
        Vector3 interactableGOPos = interactableGO.transform.position+interactableGO.interactPosition;
        interactableGOPos.y = transform.position.y;
        isTryingToInteract = true;
        
        while (Vector3.Distance(transform.position,interactableGOPos) > 0.1f) {
            yield return null;
        }
        
        isTryingToInteract = false;
        Interact(interactableGO);
    }

    public void StopInteract() {
        if (isTryingToInteract) StopCoroutine(interactCR);
    }

    private void Interact(IInteractable interactableGO) {
        isInteracting = true;
        UIManager.Instance.CloseAllPanel();
        interactableGO.Interact();
    }
}
