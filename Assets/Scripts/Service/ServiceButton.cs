public class ServiceButton : IInteractable {

    void Start() {
        ServiceManager.Instance.UpdateWindow();
    }
    
    public override void Interact() {
        ServiceManager.Instance.LoadMenu();
    }
}
