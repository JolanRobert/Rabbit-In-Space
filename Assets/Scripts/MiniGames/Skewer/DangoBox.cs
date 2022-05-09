using UnityEngine;

namespace Skewer {
public class DangoBox : MonoBehaviour {
    [SerializeField] private GameObject dangoPrefab;

    private void OnMouseDown() {
        GameObject dango = Instantiate(dangoPrefab, transform.position, Quaternion.identity);
        dango.GetComponent<DraggableDango>().FocusDango();
    }
}
}