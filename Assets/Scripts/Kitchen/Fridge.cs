public class Fridge : IInteractable {
    
    public override void Interact() {
        UIManager.Instance.OpenPanel(interactPanel);
    }
}
