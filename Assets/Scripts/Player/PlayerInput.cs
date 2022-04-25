using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour {

    private PlayerManager playerManager;
    private Camera mainCamera;
    
    /*[SerializeField] private bool activateTouch;
    [SerializeField] private bool activateMouse; //Mouse only*/

    private void Start() {
        mainCamera = Camera.main;
        playerManager = PlayerManager.Instance;
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsMouseOverUI()) {
            ScreenToRay(Input.mousePosition);
        }
    }

    private bool IsMouseOverUI() {
        return Input.touchCount > 0 ? EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) : EventSystem.current.IsPointerOverGameObject();
    }

    /*private void HandleTouch() {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
            
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            ScreenToRay(touch.position);
        }
    }*/

    /*private void HandleMouse() {
        //if (Input.touchCount > 0) return;
    }*/

    private void ScreenToRay(Vector2 screenPosition) {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        
        //Raycast hit nothing
        if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) return;
        
        //Raycast hit a non-Interactable object
        if (hit.collider.GetComponent<IInteractable>() == null) {
            playerManager.GetMovement().Move(hit.point);
            return;
        }

        //Raycast hit an Interactable object
        playerManager.GetMovement().Move(hit.collider.transform.position+hit.collider.GetComponent<IInteractable>().interactPosition);
        playerManager.GetInteract().TryInteract(hit.collider.GetComponent<IInteractable>());
    }
}
