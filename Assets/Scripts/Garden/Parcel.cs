using UnityEngine;

public class Parcel : InteractableElement {
    
    [SerializeField] private Transform parcelUI;
    public Food[] foodSlots;

    void Start() {
        foodSlots = new[]{
            interactPanel.AddComponent<Food>(),
            interactPanel.AddComponent<Food>(),
            interactPanel.AddComponent<Food>()};
        
        for (int i = 0; i < foodSlots.Length; i++) foodSlots[i].CreateFood(parcelUI.GetChild(0).GetChild(i));
        
        elementType = ElementType.PARCEL;
    }

    public override void Interact() {
        UIManager.Instance.OpenPanel(interactPanel);
        GardenManager.Instance.SelectParcel(this);
    }
}
