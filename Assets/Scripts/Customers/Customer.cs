using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour {

    [SerializeField] private SpriteRenderer customerSR;

    public CustomerSO myCustomer;
    public RecipeSO myRecipe;

    private float impatienceLimit;
    public float impatienceFactor = 1;

    public int xpReward;

    private bool hasOrdered;

    private Sprite[] customerSprites;
    private Sprite[] customerHeadSprites;

    public void Init(CustomerSO cSo) {
        myCustomer = cSo;

        customerSprites = cSo.customerHeadSprites;
        customerHeadSprites = cSo.customerSprites;
        customerSR.sprite = customerSprites[0];
        
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

        myRecipe = myCustomer.customerType switch {
            CustomerType.NORMAL => myMenu.GetRandomRecipe(),
            CustomerType.HUPPE => myMenu.GetExpensiveRecipe(),
            CustomerType.RADIN => myMenu.GetCheapRecipe(),
            CustomerType.COPIEUR => cSpawner.customerQueue[cSpawner.customerQueue.Count-1].myRecipe,
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

    public void TryCompleteOrder() {
        if (!FoodDataManager.Instance.HasRecipeItem(myRecipe.recipeType)) return;
        
        for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
            FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
            
            if (item.recipeType != myRecipe.recipeType) continue;
            item.amount -= 1;
            UIKitchen.Instance.UpdateWorkplanSlot(i,item.amount);
            break;
        }
        
        CompleteOrder(true);
    }

    private void CompleteOrder(bool success) {
        KitchenManager.Instance.customerSpawner.DepopCustomer(this);
    }

    public void CancelOrder() {
        CustomerOrderManager.Instance.RemoveCustomerOrder(this);
    }
}