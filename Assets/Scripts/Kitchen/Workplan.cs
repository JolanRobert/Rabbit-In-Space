public class Workplan : InteractableElement {

    public override void Interact() {
        KitchenUI.Instance.OpenWorkplan();
    }
}