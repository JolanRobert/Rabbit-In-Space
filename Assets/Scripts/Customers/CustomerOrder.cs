using DG.Tweening;
using UnityEngine;

public class CustomerOrder : MonoBehaviour {

    [SerializeField] private SpriteRenderer customerChoiceSR;
    [SerializeField] private Transform impatienceProgress;
    [SerializeField] private SpriteRenderer impatienceColorBarSR;

    public void Init(RecipeSO order) {
        customerChoiceSR.sprite = order.recipeSprite;
    }

    private Color newColor = new Color(1,1,0,1);
    private Vector3 newScale;
    public void UpdateImpatienceProgress(float value) {
        newScale = transform.localScale;
        newScale.x = value;
        impatienceProgress.DOScale(new Vector3(value, impatienceProgress.localScale.y, impatienceProgress.localScale.z),0.2f);
        
        newColor.r = 1 - value;
        newColor.g = value;
        impatienceColorBarSR.color = newColor;
    }
}
