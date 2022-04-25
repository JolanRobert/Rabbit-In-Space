using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
        }
    }
    
    public void ChangeScene(string newScene) {
        StartCoroutine(LoadAsyncScene(newScene));
    }

    private IEnumerator LoadAsyncScene(string nextScene) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone) {
            yield return null;
        }
    }
}
