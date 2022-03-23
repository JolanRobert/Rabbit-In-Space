public class MetallicCurtain : InteractableElement {
    
    public override void Interact() {
        KitchenManager.Instance.inService = !KitchenManager.Instance.inService;
        
        if (KitchenManager.Instance.inService) KitchenManager.Instance.customerSpawner.StartService();
        else KitchenManager.Instance.customerSpawner.EndService();
        
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
