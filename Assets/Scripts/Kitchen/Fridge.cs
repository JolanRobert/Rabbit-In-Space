public class Fridge : InteractableElement {

    public override void Interact() {
        KitchenUI.Instance.OpenFridge();
    }
}
