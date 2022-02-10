using UnityEngine;

public class Parcel : InteractableElement {
    
    [SerializeField] private GameObject[] plants;
    public Food[] foodSlots;

    void Start() {
        foodSlots = new[]{new Food(plants[0]),new Food(plants[1]),new Food(plants[2])};
        elementType = ElementType.PARCEL;
    }

    public override void Interact() {
        UIManager.Instance.OpenPanel(interactPanel);
        GardenManager.Instance.SelectParcel(this);
    }
}
