using UnityEngine;
using UnityEngine.UI;

public class BookStationSlot : MonoBehaviour {
    
    public Image slotSprite;
    
    public void Init(StationSO sSo) {
        slotSprite.sprite = sSo.icon;
    }
}
