public class Fridge : InteractableElement {

    public override void Interact() {
        UIManager.Instance.OpenFridge();
    }
}
