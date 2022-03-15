using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    
    //Interaction
    public Coroutine interactCR { get; private set; }
    public bool isTryingToInteract { get; private set; }
    public bool isInteracting { get; set; }
    
    public void TryInteract(InteractableElement interactableGO) {
        if (isTryingToInteract) StopCoroutine(interactCR);
        interactCR = StartCoroutine(InteractCR(interactableGO));
    }

    private IEnumerator InteractCR(InteractableElement interactableGO) {
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

    private void Interact(InteractableElement interactableGO) {
        interactableGO.Interact();
        isInteracting = true;
    }
}
