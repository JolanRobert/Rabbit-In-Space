using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelMenuEntry : MonoBehaviour {
    
    public Image plantSprite;
    public TMP_Text plantName;
    public Image growthFill;
    public Image deadFill;
    public TMP_Text growthText;
    
    public void Reset() {
        plantSprite.sprite = null;
        plantSprite.rectTransform.sizeDelta = new Vector2(250, 250);
        
        plantName.text = "";
        
        growthFill.DOKill();
        growthFill.fillAmount = 0;
        
        deadFill.DOKill();
        deadFill.fillAmount = 0;
        
        growthText.text = "";
    }
}
