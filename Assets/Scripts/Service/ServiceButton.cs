public class ServiceButton : IInteractable {
    
    public override void Interact() {
        ServiceManager.Instance.LoadMenu();
    }
}
