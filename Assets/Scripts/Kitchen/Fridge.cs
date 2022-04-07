public class Fridge : InteractableElement {

    public override void Interact() {
        UIManager.Instance.OpenPanel(InventoryManager.fridgeInstance.inventory);
    }
}
