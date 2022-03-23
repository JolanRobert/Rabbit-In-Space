public class BookStation : InteractableElement
{
    public override void Interact() {
        if (KitchenManager.Instance.inService)
        {
            KitchenUI.Instance.OpenBook(interactPanel.gameObject);
        }
        else
        {
            PlayerManager.Instance.GetInteract().isInteracting = false;
        }
    }
}
