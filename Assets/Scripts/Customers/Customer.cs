using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : IInteractable {

    [SerializeField] private CustomerOrder customerOrder;
    [SerializeField] private SpriteRenderer customerSR;

    private RecipeSO myOrder;
    public CustomerType customerType;
    
    private float impatienceLimit;
    public float impatienceFactor = 1;

    public int xpReward;

    private bool hasOrdered;

    public void Init(CustomerSO cSo) {
        customerSR.sprite = cSo.customerSprite;
        customerType = cSo.customerType;
        impatienceLimit = cSo.impatienceLimit;
        xpReward = cSo.xpReward;
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
        if (!FoodDataManager.Instance.HasRecipeItem(myOrder.recipeType)) return;
        
        for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
            FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
            
            if (item.recipeType != myOrder.recipeType) continue;
            item.amount -= 1;
            UIKitchen.Instance.UpdateWorkplanSlot(i,item.amount);
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