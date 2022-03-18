using UnityEngine;

public class CustomerOrder : MonoBehaviour {

    [SerializeField] private SpriteRenderer customerChoiceSR;
    [SerializeField] private Transform impatienceProgress;
    [SerializeField] private SpriteRenderer impatienceColorBarSR;
    [SerializeField] private SpriteRenderer inStockSR;

    public void Init(RecipeSO order) {
        customerChoiceSR.sprite = order.recipeSprite;
    }

    public void UpdateImpatienceProgress(int value) {
        impatienceProgress.localScale = new Vector3(value, impatienceProgress.localScale.y, impatienceProgress.localScale.z);
        impatienceColorBarSR.color = new Color(1 - value, value, 0, 1);
    }

    public void UpdateInStock(bool inStock) {
        inStockSR.color = inStock ? Color.green : Color.red;
    }
}
