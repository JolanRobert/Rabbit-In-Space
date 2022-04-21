public class Workplan : IInteractable {
    
    public override void Interact() {
        UIManager.Instance.OpenPanel(interactPanel);
    }
}
