public class Counter : InteractableElement {
    
    public override void Interact() {
        if (KitchenManager.Instance.inService) {
            CustomerSpawner customerSpawner = KitchenManager.Instance.customerSpawner;
            for (int i = customerSpawner.nbCounterCustomer-1; i < customerSpawner.nbCounterCustomer; i--) {
                TryCompleteCustomerOrder(customerSpawner.customerQueue[i]);
            }
        }

        PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    private void TryCompleteCustomerOrder(Customer customer) {
        if (!FoodDataManager.Instance.HasRecipeItem(customer.GetOrder().recipeType)) {
            return;
        }
        
        foreach (InventoryManager.RecipeItem item in RecipeManager.instance.serviceRecipes) {
            if (item.rSo.recipeType != customer.GetOrder().recipeType) continue;
            item.amount--;
            break;
        }
        customer.CompleteOrder(true);
    }
}
