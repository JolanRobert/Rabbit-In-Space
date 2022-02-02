using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private PlayerActions playerActions;
    private Camera mainCamera;
    
    [SerializeField] private bool activateMouse; //Mouse only

    void Awake() {
        playerActions = GetComponent<PlayerActions>();
        mainCamera = Camera.main;
    }
    
    void Update() {
        HandleTouch();
        HandleMouse();
    }

    private void HandleTouch() {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
            
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            Vector3 newPosition = ScreenToRay(touch.position);
            playerActions.Move(newPosition);
        }
    }

    private void HandleMouse() {
        if (!activateMouse) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 newPosition = ScreenToRay(Input.mousePosition);
            playerActions.Move(newPosition);
        }
    }

    private Vector3 ScreenToRay(Vector2 screenPosition) {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) return hit.point+Vector3.up;
        return Vector3.negativeInfinity;
    }
}
