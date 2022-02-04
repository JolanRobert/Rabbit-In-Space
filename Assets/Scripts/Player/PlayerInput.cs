using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private PlayerManager playerManager;
    private Camera mainCamera;
    
    [SerializeField] private bool activateTouch;
    [SerializeField] private bool activateMouse; //Mouse only

    private void Start() {
        mainCamera = Camera.main;
        playerManager = PlayerManager.instance;
    }
    
    private void Update() {
        if (activateTouch) HandleTouch();
        else if (activateMouse) HandleMouse();
    }

    private void HandleTouch() {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
            
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            ScreenToRay(touch.position);
        }
    }

    private void HandleMouse() {
        if (Input.touchCount > 0) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ScreenToRay(Input.mousePosition);
        }
    }

    private void ScreenToRay(Vector2 screenPosition) {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        //Check if Raycast hit an object
        if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) {
            playerManager.GetMovement().Move(Vector3.negativeInfinity);
            return;
        }
        
        //Raycast hit a non-Interactable object
        if (hit.collider.GetComponent<IInteractable>() == null) {
            playerManager.GetMovement().Move(hit.point);
            return;
        }
        
        //Raycast hit an Interactable object
        playerManager.GetMovement().Move(hit.collider.GetComponent<IInteractable>().InteractPosition);
        playerManager.GetInteract().TryInteract(hit.collider.GetComponent<IInteractable>());
    }
}
