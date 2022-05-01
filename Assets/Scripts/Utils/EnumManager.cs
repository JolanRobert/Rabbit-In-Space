using UnityEngine;

public class EnumManager : MonoBehaviour {

    public FoodType itemType;
}

public enum StationType {
    BOILER, MOCHI_BEATER, CUTTER, SKEWER, GRILL, TRIMMING, ROLLING_PIN, MIXER
}

public enum FoodType {
    NONE, MOON_RICE, STARBERRY, NEBULAZUKI
}

public enum RecipeType {
    NONE, STARBERRY_DAIFUKU, HANAMI_DANGOS, MOCHI_WAFFLE, STARBERRY_BUBBLE_TEA, KINAKO_MOCHI
}

public enum UpgradeType {
    ENGRAIS, NUTRIMENTS, RECOLTOUT, GRAINATOR
}

public enum CustomerType {
    NORMAL, HUPPE, RADIN, COPIEUR, ACCRO, LENT, IMPATIENT, ENERVANT
}

public enum TutorialPart
{
    ACCESS_GARDEN, GROW_RICE, START_SERVICE, START_RECIPE, START_MINI_GAME, SERVE, END_SERVICE
}