using UnityEngine;

public class EnumManager : MonoBehaviour {

    public FoodType itemType;
}

public enum StockType {
    FRIDGE, WORKPLAN
}

public enum StationType {
    BOILER, MOCHI_BEATER
}

public enum FoodType {
    NONE, MOON_RICE, STARBERRY, NEBULAZUKI
}

public enum RecipeType {
    NONE, STARBERRY_DAIFUKU, HANAMI_DANGOS, MOCHI_WAFFLE, STARBERRY_BUBBLE_TEA, KINAKO_MOCHI
}

public enum SceneName {
    FRONT_TRUCK, KITCHEN, GARDEN
}

public enum UpgradeType {
    ENGRAIS, NUTRIMENTS, RECOLTOUT, GRAINATOR
}

public enum CustomerType {
    NORMAL, HUPPE, RADIN, COPIEUR, ACCRO, LENT, IMPATIENT, ENERVANT
}