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

        customerSprites = cSo.customerSprites;
        customerHeadSprites = cSo.customerHeadSprites;
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
        int m_impatienceLimit = (int)impatienceLimit;
        while (impatienceLimit > 0) {
            yield return new WaitForSeconds(1);
            impatienceLimit -= 1 * impatienceFactor;
            if ((int) impatienceLimit == m_impatienceLimit*2/3) {
                customerSR.sprite = customerSprites[1];
                CustomerOrderManager.Instance.UpdateCustomerOrder(this,customerHeadSprites[1]);
            }
            else if ((int) impatienceLimit == m_impatienceLimit*1/3) {
                customerSR.sprite = customerSprites[2];
                CustomerOrderManager.Instance.UpdateCustomerOrder(this,customerHeadSprites[2]);
            }
        }
        
        CompleteOrder(CustomerState.LEFT);
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
        
        CompleteOrder(CustomerState.SERVED);
    }

    private void CompleteOrder(CustomerState state) {
        ServiceManager.Instance.serviceSummary.NewServiceInfo(this,state);
        KitchenManager.Instance.customerSpawner.DepopCustomer(this);
    }

    public void CancelOrder() {
        CustomerOrderManager.Instance.RemoveCustomerOrder(this);
    }
}