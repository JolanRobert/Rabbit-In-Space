using UnityEngine;

namespace Grill {
public class Bellows : MonoBehaviour {

    [SerializeField] private HeatBar heatBar;
    [Range(1,100)] [SerializeField] private int heatUpValue;

    private void OnMouseDown() {
        heatBar.ChangeHeat(heatUpValue);
    }
}
}

