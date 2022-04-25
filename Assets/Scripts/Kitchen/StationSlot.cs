using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StationSlot : MonoBehaviour {
    
    [SerializeField] private Image slotSprite;
    
    public void Init(StationSO sSo) {
        slotSprite.sprite = sSo.icon;
    }
}
