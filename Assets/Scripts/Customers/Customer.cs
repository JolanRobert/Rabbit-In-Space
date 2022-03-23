using System;
using System.Collections;
using UnityEngine;

public class Customer : InteractableElement {

    [SerializeField] private CustomerOrder customerOrder;
    [SerializeField] private SpriteRenderer customerSR;

    private RecipeSO myOrder;
    public CustomerType customerType;
    
    private float impatienceLimit;
    public float impatienceFactor = 1;

    private bool hasOrdered;

    void Start() {
        interactPosition = -transform.position + new Vector3(-3, 0, -1);
    }

    public void Init(CustomerSO cSo) {
        customerSR.sprite = cSo.customerSprite;
        customerType = cSo.customerType;
        impatienceLimit = cSo.impatienceLimit;
    }

    public void MakeOrder() {
        if (hasOrdered) return;
        hasOrdered = true;

        myOrder = customerType switch {
            CustomerType.NORMAL => KitchenManager.Instance.myMenu.GetRandomRecipe(),
            CustomerType.HUPPE => KitchenManager.Instance.myMenu.GetExpensiveRecipe(),
            CustomerType.RADIN => KitchenManager.Instance.myMenu.GetCheapRecipe(),
            CustomerType.COPIEUR => KitchenManager.Instance.customerSpawner.customerQueue[0].GetOrder(),
            CustomerType.ACCRO => KitchenManager.Instance.myMenu.GetTrueRandomRecipe(),
            CustomerType.LENT => KitchenManager.Instance.myMenu.GetRandomRecipe(),
            CustomerType.IMPATIENT => KitchenManager.Instance.myMenu.GetRandomRecipe(),
            CustomerType.ENERVANT => KitchenManager.Instance.myMenu.GetRandomRecipe(),
            _ => throw new Exception("Unknown customer type")
        };

        customerOrder.gameObject.SetActive(true);
        customerOrder.Init(myOrder);
        StartCoroutine(Leave());
    }
    
    private IEnumerator Leave() {
        float m_impatienceLimit = impatienceLimit;
        while (impatienceLimit > 0) {
            yield return new WaitForSeconds(1);
            impatienceLimit -= 1 * impatienceFactor;
            customerOrder.UpdateImpatienceProgress(impatienceLimit/m_impatienceLimit);
        }
        
        CompleteOrder(false);
    }

    private void TryCompleteOrder() {
        if (!FoodDataManager.Instance.HasRecipeItem(myOrder.recipeType)) {
            return;
        }
        
        foreach (InventoryManager.RecipeItem item in RecipeManager.instance.serviceRecipes) {
            if (item.rSo.recipeType != myOrder.recipeType) continue;
            item.amount--;
            break;
        }
        CompleteOrder(true);
    }

    private void CompleteOrder(bool success) {
        KitchenManager.Instance.customerSpawner.DepopCustomer(this);
    }

    private RecipeSO GetOrder() {
        return myOrder;
    }
    
    public override void Interact() {
        CustomerSpawner customerSpawner = KitchenManager.Instance.customerSpawner;
        for (int i = 0; i < customerSpawner.nbCounterCustomer; i++) {
            if (customerSpawner.customerQueue[i] != this) continue;
            TryCompleteOrder();
            break;
        }

        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}