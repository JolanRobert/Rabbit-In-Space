using UnityEngine;

namespace Cutter {
    
    public class PlayerInput : MonoBehaviour {
        
        private Camera mainCamera;

        private void Start() {
            mainCamera = Camera.main;
        }
    
        private void Update() {
            if (Input.GetKey(KeyCode.Mouse0)) {
                ScreenToRay(Input.mousePosition);
            }
        }

        private void ScreenToRay(Vector2 screenPosition) {
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) return;

            if (hit.collider.GetComponent<Dough>()) hit.collider.GetComponent<Dough>().Slice();
        }
    }
}