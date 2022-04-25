using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : IInteractable {

    [SerializeField] private SpriteRenderer customerSR;

    public RecipeSO myOrder;
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

    private MenuGenerator myMenu;
    private CustomerSpawner cSpawner;
    public void MakeOrder() {
        if (hasOrdered) return;
        hasOrdered = true;

        myMenu = KitchenManager.Instance.myMenu;
        cSpawner = KitchenManager.Instance.customerSpawner;

        myOrder = customerType switch {
            CustomerType.NORMAL => myMenu.GetRandomRecipe(),
            CustomerType.HUPPE => myMenu.GetExpensiveRecipe(),
            CustomerType.RADIN => myMenu.GetCheapRecipe(),
            CustomerType.COPIEUR => cSpawner.customerQueue[cSpawner.customerQueue.Count-1].GetOrder(),
            CustomerType.ACCRO => myMenu.GetTrueRandomRecipe(),
            CustomerType.LENT => myMenu.GetRandomRecipe(),
            CustomerType.IMPATIENT => myMenu.GetRandomRecipe(),
            CustomerType.ENERVANT => myMenu.GetRandomRecipe(),
            _ => throw new Exception("Unknown customer type")
        };
        
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