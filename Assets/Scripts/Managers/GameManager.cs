using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*void Start() {
        SwitchScene.Instance.ToGarden();
    }*/
}
