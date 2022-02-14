public class Fridge : InteractableElement
{
    void Start()
    {
        elementType = ElementType.FRIDGE;
    }

    public override void Interact() {
        UIManager.Instance.OpenFridge();
    }
}
