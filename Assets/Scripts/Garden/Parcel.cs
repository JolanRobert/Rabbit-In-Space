public class Parcel : InteractableElement {
    
    public Food[] foodList;

    void Start() {
        foodList = new[] {
            gameObject.AddComponent<Food>(),
            gameObject.AddComponent<Food>(),
            gameObject.AddComponent<Food>()
        };

        for (int i = 0; i < foodList.Length; i++) {
            foodList[i].InitFoodUI(interactPanel.transform.GetChild(i));
        }
    }
    
    public override void Interact() {
        UIGarden.Instance.OpenParcel(interactPanel);
        GardenManager.Instance.SelectParcel(this);
    }
}
