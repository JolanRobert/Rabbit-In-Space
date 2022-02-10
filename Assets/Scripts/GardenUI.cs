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

    public void SetPlantSprite(GameObject plant, Sprite newSprite) {
        Transform plantSprite = plant.transform.GetChild(0);
        plantSprite.GetComponent<Image>().sprite = newSprite;
    }
    
    public void SetPlantName(GameObject plant, string newName) {
        Transform plantName = plant.transform.GetChild(1);
        plantName.GetComponent<TMP_Text>().text = newName;
    }

    public void SetPlantGrowthTime(GameObject plant, int newGrowthTime) {
        Transform plantGrowthTime = plant.transform.GetChild(2);
        plantGrowthTime.GetComponent<TMP_Text>().text = "" + newGrowthTime;
    }
}
