public class Parcel : InteractableElement {
    
    public Food[] foodList;
    
    public override void Interact() {
        GardenManager.Instance.SelectParcel(this);
        UIGarden.Instance.OpenParcelMenu();
    }
}
