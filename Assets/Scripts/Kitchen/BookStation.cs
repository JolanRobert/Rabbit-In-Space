public class BookStation : InteractableElement
{
    public override void Interact() {
        KitchenUI.Instance.OpenBook(interactPanel.gameObject); //Faudra faire une nouvelle fonction, openminigame ça correspond pas
    }
}
