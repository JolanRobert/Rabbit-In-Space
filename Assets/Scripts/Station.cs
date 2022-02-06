public class Station : InteractableElement {

    void Start() {
        elementType = ElementType.STATION;
    }

    public override void Interact() {
        base.Interact();
        GameManager.instance.StartMinigame();
    }
}
