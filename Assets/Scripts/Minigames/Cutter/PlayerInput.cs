using UnityEngine;

namespace Cutter {
    
    public class PlayerInput : MonoBehaviour {
        
        private Camera mainCamera;
        [SerializeField] private TrailRenderer trailRenderer;

        private void Start() {
            mainCamera = Camera.main;
        }
    
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,-mainCamera.transform.position.z));
                trailRenderer.Clear();
            }
            
            if (Input.GetKey(KeyCode.Mouse0)) {
                ScreenToRay(Input.mousePosition);
                transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,-mainCamera.transform.position.z));
            }
        }

        private void ScreenToRay(Vector2 screenPosition) {
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) return;

            if (hit.collider.TryGetComponent(out Dough dough)) dough.Slice();
        }
    }
}