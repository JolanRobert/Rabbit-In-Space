public class Fridge : IInteractable {
    
    public override void Interact() {
        UIKitchen.Instance.OpenFridge();
    }
}
