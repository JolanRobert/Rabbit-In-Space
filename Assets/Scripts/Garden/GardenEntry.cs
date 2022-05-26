using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GardenEntry : MonoBehaviour {
    
    [Header("Animation")]
    [SerializeField] private RectTransform topSprite;
    [SerializeField] private float openTime = 0.5f;
    private float openYPos, baseYPos;
    
    private bool isOpen;
    
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
}
