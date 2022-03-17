public class BookStation : InteractableElement
{
    public override void Interact() {
        UIManager.Instance.OpenMinigame(interactPanel.gameObject); //Faudra faire une nouvelle fonction, openminigame ça correspond pas
    }
}
