using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : IInteractable {

    [SerializeField] private SpriteRenderer customerSR;

    public CustomerSO customer;
    public RecipeSO recipe;

    private float impatienceLimit;
    public float impatienceFactor = 1;

    public int xpReward;

    private bool hasOrdered;

    public void Init(CustomerSO cSo) {
        customer = cSo;
        
        customerSR.sprite = cSo.customerSprite;
        impatienceLimit = cSo.impatienceLimit;
        xpReward = cSo.xpReward;
    }

    private MenuGenerator myMenu;
    private CustomerSpawner cSpawner;
    public void MakeOrder() {
        if (hasOrdered) return;
        hasOrdered = true;

        myMenu = KitchenManager.Instance.myMenu;
        cSpawner = KitchenManager.Instance.customerSpawner;

        recipe = customer.customerType switch {
            CustomerType.NORMAL => myMenu.GetRandomRecipe(),
            CustomerType.HUPPE => myMenu.GetExpensiveRecipe(),
            CustomerType.RADIN => myMenu.GetCheapRecipe(),
            CustomerType.COPIEUR => cSpawner.customerQueue[cSpawner.customerQueue.Count-1].recipe,
            CustomerType.ACCRO => myMenu.GetTrueRandomRecipe(),
            CustomerType.LENT => myMenu.GetRandomRecipe(),
            CustomerType.IMPATIENT => myMenu.GetRandomRecipe(),
            CustomerType.ENERVANT => myMenu.GetRandomRecipe(),
            _ => throw new Exception("Unknown customer type")
        };

        CustomerOrderManager.Instance.AddCustomerOrder(this);
        StartCoroutine(Leave());
    }
    
    private IEnumerator Leave() {
        float m_impatienceLimit = impatienceLimit;
        while (impatienceLimit > 0) {
            yield return new WaitForSeconds(1);
            impatienceLimit -= 1 * impatienceFactor;
        }
        
        CompleteOrder(false);
    }

    private void TryCompleteOrder() {
        if (!FoodDataManager.Instance.HasRecipeItem(recipe.recipeType)) return;
        
        for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
            FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
            
            if (item.recipeType != recipe.recipeType) continue;
            item.amount -= 1;
            UIKitchen.Instance.UpdateWorkplanSlot(i,item.amount);
            break;
        }
        
        CompleteOrder(true);
    }

    private void CompleteOrder(bool success) {
        CustomerOrderManager.Instance.RemoveCustomerOrder(this);
        KitchenManager.Instance.customerSpawner.DepopCustomer(this);
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