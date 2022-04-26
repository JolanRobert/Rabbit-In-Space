using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderEntry : MonoBehaviour {

    public Customer customer;
    
    [SerializeField] private Image customerSR;
    [SerializeField] private Image orderSR;

    public void Init(Customer customer) {
        this.customer = customer;

        customerSR.sprite = customer.myCustomer.customerSprites[0];
        orderSR.sprite = customer.myRecipe.recipeSprite;
    }

    public void TryCompleteOrder() {
        customer.TryCompleteOrder();
    }
}
