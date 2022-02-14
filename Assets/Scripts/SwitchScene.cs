using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    private GameManager gameManager;
    [SerializeField] private EnumManager.SceneType toScene;
    
    public void Switch() {
        string nextScene = toScene switch {
            EnumManager.SceneType.FRONT_TRUCK => "Front_Truck",
            EnumManager.SceneType.KITCHEN => "Kitchen",
            EnumManager.SceneType.GARDEN => "Garden",
            _ => throw new ArgumentOutOfRangeException()
        };

        SceneManager.LoadScene(nextScene);
    }
}
