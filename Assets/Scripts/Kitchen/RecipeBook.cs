public class RecipeBook : IInteractable {
    
    public override void Interact() {
        if (KitchenManager.Instance.inService) UIManager.Instance.OpenPanel(interactPanel);
        else PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
