public class Workplan : InteractableElement
{
    void Start()
    {
        elementType = ElementType.WORKPLAN;
    }

    public override void Interact() {
        UIManager.Instance.OpenWorkplan();
    }
}