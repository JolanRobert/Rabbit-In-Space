public class Workplan : InteractableElement {

    public override void Interact() {
        UIManager.Instance.OpenPanel(InventoryManager.workplanInstance.inventory);
    }
}