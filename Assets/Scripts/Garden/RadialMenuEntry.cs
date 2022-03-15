using UnityEngine;
using UnityEngine.UI;

public class RadialMenuEntry : MonoBehaviour {

    [SerializeField] private Image foodSprite;

    private ItemType itemType;

    public void Init(FoodSO foodSo) {
        foodSprite.sprite = foodSo.foodSprite;
        itemType = foodSo.itemType;
    }

    public void SelectFood() {
        Debug.Log(itemType);
    }
}
