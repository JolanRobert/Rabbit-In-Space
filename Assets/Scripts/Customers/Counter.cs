public class Counter : InteractableElement {
    
    public override void Interact() {
        TryCompleteCustomerOrder(KitchenManager.Instance.customerSpawner.customerQueue[1]);
        TryCompleteCustomerOrder(KitchenManager.Instance.customerSpawner.customerQueue[0]);
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    private void TryCompleteCustomerOrder(Customer customer) {
        if (!FoodDataManager.Instance.HasRecipeItem(customer.GetOrder().recipeType)) {
            return;
        }
        
        foreach (InventoryManager.RecipeItem item in InventoryManager.workplanInstance.serviceRecipes) {
            if (item.rSo.recipeType != customer.GetOrder().recipeType) continue;
            item.amount--;
            break;
        }
        customer.CompleteOrder(true);
    }
}
