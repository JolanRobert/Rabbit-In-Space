public class Counter : InteractableElement {
    
    public override void Interact() {
        KitchenManager.Instance.customerSpawner.customerQueue[0].CompleteOrder(true);
    }
}
