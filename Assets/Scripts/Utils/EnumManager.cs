using UnityEngine;

public class EnumManager : MonoBehaviour {

    public ItemType itemType;
}

public enum StockType {
    FRIDGE, WORKPLAN
}

public enum StationType {
    BOILER, MOCHI_BEATER
}
    
public enum ItemType {
    NONE, MOON_RICE, STARBERRY, NEBULAZUKI, STARBERRY_DAIFUKU, HANAMI_DANGOS, MOCHI_WAFFLE, STARBERRY_BUBBLE_TEA
}

public enum SceneName {
    FRONT_TRUCK, KITCHEN, GARDEN
}

public enum UpgradeType {
    ENGRAIS, NUTRIMENTS, RECOLTOUT, GRAINATOR
}