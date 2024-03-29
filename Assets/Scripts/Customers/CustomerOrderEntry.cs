using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderEntry : MonoBehaviour {

    public Customer customer;

    [SerializeField] private Image backgroundSR;
    [SerializeField] private Image customerSR;
    [SerializeField] private Image orderSR;
    [SerializeField] private TMP_Text ownedRecipeAmountText;

    private Tween colorTween;

    public void Init(Customer customer) {
        this.customer = customer;

        customerSR.sprite = customer.myCustomer.customerHeadSprites[0];
        orderSR.sprite = customer.myRecipe.recipeSprite;
        UpdateOwnedRecipeAmount();
    }

    void Update() {
        if (colorTween == null) return;
        if (colorTween.IsPlaying() && !GameManager.Instance.timeElapsing) colorTween.Pause();
        else if (!colorTween.IsPlaying() && GameManager.Instance.timeElapsing) colorTween.Play();
    }

    public void UpdateSprite(Sprite newSprite) {
        customerSR.sprite = newSprite;
    }

    public void UpdateBackground(float timeLeft, float impatienceFactor) {
        colorTween = backgroundSR.DOColor(new Color(255 / 255f, 50 / 255f, 50 / 255f, 1), timeLeft / impatienceFactor);
    }

    public void ResetBackground() {
        backgroundSR.DOKill();
        backgroundSR.color = new Color(50 / 255f, 255 / 255f, 50 / 255f, 1);
    }

    public void UpdateOwnedRecipeAmount() {
        ownedRecipeAmountText.text = $"Owned : {InventoryManager.Instance.GetAmountOfRecipeItem(customer.myRecipe)}";
    }

    public void TryCompleteOrder() {
        customer.TryCompleteOrder();
    }

    public void ForceServe() {
        customer.CompleteOrder(CustomerState.SERVED);
    }
}
