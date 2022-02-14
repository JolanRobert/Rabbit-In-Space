using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    private GameManager gameManager;
    [SerializeField] private SceneName toScene;
    
    public void Switch() {
        string nextScene = toScene switch {
            SceneName.FRONT_TRUCK => "Front_Truck",
            SceneName.KITCHEN => "Kitchen",
            SceneName.GARDEN => "Garden",
            _ => throw new ArgumentOutOfRangeException()
        };

        SceneManager.LoadScene(nextScene);
    }
}
