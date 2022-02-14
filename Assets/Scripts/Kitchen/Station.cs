public class Station : InteractableElement {

    void Start() {
        elementType = ElementType.STATION;
    }

    public override void Interact() {
        UIManager.Instance.OpenMinigame(interactPanel);
    }
}
