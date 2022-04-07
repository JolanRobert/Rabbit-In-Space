public class MetallicCurtain : InteractableElement {
    
    public override void Interact() {
        if (!ServiceManager.Instance.inService) KitchenManager.Instance.customerSpawner.StartService();
        else KitchenManager.Instance.customerSpawner.EndService();
        
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
