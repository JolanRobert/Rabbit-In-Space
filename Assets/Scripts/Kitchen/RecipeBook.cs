public class RecipeBook : InteractableElement {
    
    public override void Interact() {
        if (ServiceManager.Instance.inService) UIManager.Instance.OpenPanel(interactPanel.gameObject);
        else PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
