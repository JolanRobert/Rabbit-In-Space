using UnityEngine;
using UnityEngine.UI;

public class StationSlot : MonoBehaviour {
    
    public Image slotSprite;
    
    public void Init(StationSO sSo) {
        slotSprite.sprite = sSo.icon;
    }
}
