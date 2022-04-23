using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceEntry : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private Image recipeSprite;
    [SerializeField] private TMP_Text recipePrice;

    public void Init(RecipeSO rSo) {
        recipeSprite.sprite = rSo.recipeSprite;
        recipePrice.text = ""+rSo.price+"$";
    }
}
