using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderEntry : MonoBehaviour {

    public Customer customer;

    [SerializeField] private Image backgroundSR;
    [SerializeField] private Image customerSR;
    [SerializeField] private Image orderSR;

    public void Init(Customer customer) {
        this.customer = customer;

        customerSR.sprite = customer.myCustomer.customerHeadSprites[0];
        orderSR.sprite = customer.myRecipe.recipeSprite;
    }

    public void UpdateSprite(Sprite newSprite) {
        customerSR.sprite = newSprite;
    }

    public void TryCompleteOrder() {
        customer.TryCompleteOrder();
    }
}
