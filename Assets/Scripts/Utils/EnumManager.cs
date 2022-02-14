using UnityEngine;

public class EnumManager : MonoBehaviour {
    
    public enum StockType
    {
        FRIDGE, WORKPLAN
    }
    
    public enum ItemType
    {
        NONE, MOON_RICE, STARBERRY, NEBULAZUKI, STARBERRY_DAIFUKU, HANAMI_DANGOS, MOCHI_WAFFLE, STARBERRY_BUBBLE_TEA
    }

    public enum SceneType {
        FRONT_TRUCK, KITCHEN, GARDEN
    }
}
