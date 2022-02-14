public class Station : InteractableElement {

    public override void Interact() {
        UIManager.Instance.OpenMinigame(interactPanel.gameObject);
    }
}
