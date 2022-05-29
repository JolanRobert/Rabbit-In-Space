using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GardenEntry : MonoBehaviour {
    
    [Header("Animation")]
    [SerializeField] private RectTransform topSprite;
    [SerializeField] private float openTime = 0.5f;
    private float openYPos, baseYPos;
    private bool isOpen;

    [Header("Plant States")]
    [SerializeField] private Image[] plantStateList;
    [SerializeField] private Sprite noneSprite, growSprite, endSprite, deadSprite;
    
    void Start() {
        //Init anim position
        baseYPos = topSprite.anchoredPosition.y;
        openYPos = baseYPos + 25;
    }

    public void OpenParcel() {
        if (isOpen) return;
        StartCoroutine(OpenParcelCR());
    }

    private IEnumerator OpenParcelCR() {
        if (GardenManager.Instance.myParcel != null) {
            foreach (Food food in GardenManager.Instance.myParcel.foodList) {
                food.foodUI = null;
            }
        }

        EventSystemToggler.Instance.Cut();
        topSprite.DOAnchorPosY(openYPos, openTime);
        yield return new WaitForSeconds(openTime);
        
        GardenManager.Instance.SelectParcel(this);
        UIGarden.Instance.OpenParcelMenu();
        yield return new WaitForSeconds(0.2f);
        isOpen = true;
    }

    public void CloseParcel() {
        topSprite.DOAnchorPosY(baseYPos, openTime);
        isOpen = false;
    }

    public void SetupPlantStates(Food[] foodList) {
        for (int i = 0; i < plantStateList.Length; i++) {
            if (foodList[i].GrowthLevel == -1) plantStateList[i].sprite = noneSprite;
            else if (foodList[i].GrowthLevel <= 1) plantStateList[i].sprite = growSprite;
            else if (foodList[i].GrowthLevel == 2) plantStateList[i].sprite = endSprite;
            else if (foodList[i].GrowthLevel == 3) plantStateList[i].sprite = deadSprite;
        }
    }
}
