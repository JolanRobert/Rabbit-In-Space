using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GardenUI : MonoBehaviour {

    [SerializeField] private GameObject menuSeed;
    [SerializeField] private GameObject closeOverlay;

    public void OpenMenuSeed(int foodSlot) {
        closeOverlay.SetActive(true);
        menuSeed.transform.DOMoveX(1330, 0.325f).SetEase(Ease.OutBack, 1.87f);
        GardenManager.Instance.SelectFood(foodSlot);
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        menuSeed.transform.DOMoveX(1630, 0.325f).SetEase(Ease.InBack, 1.87f);
    }

    //-----------------------------------------//
    //Plant Updater
    //-----------------------------------------//

    public void SetPlantSprite(Image mySprite, Sprite newSprite) {
        mySprite.sprite = newSprite;
        mySprite.SetNativeSize();
    }
    
    public void SetPlantName(TMP_Text myName, string newName) {
        myName.text = newName;
    }

    public void SetPlantGrowthTime(TMP_Text myProgressText, int newGrowingTime) {
        if (newGrowingTime <= 0) return;
        myProgressText.text = newGrowingTime.ToString();
    }
}
