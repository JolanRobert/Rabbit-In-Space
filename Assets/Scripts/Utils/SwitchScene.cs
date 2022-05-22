using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;

    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private float loadingAnimationTime;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    public void ChangeScene(string newScene) {
        StartCoroutine(LoadScene(newScene));
    }

    public IEnumerator LoadScene(string nextScene) {
        //Play animation
        loadingScreen.DOFade(1, loadingAnimationTime);
        yield return new WaitForSeconds(loadingAnimationTime);
        
        //Load scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        while (!operation.isDone) yield return null;
        
        //End animation
        loadingScreen.DOFade(0, loadingAnimationTime);
    }
}
