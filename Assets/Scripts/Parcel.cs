using System.Collections.Generic;

public class Parcel : InteractableElement {

    public List<Plant> plants;

    void Start() {
        interactElementType = InteractElementType.PARCEL;
    }
}
