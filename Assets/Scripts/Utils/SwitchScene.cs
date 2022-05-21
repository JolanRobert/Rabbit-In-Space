using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;
    
    private Vector3 savePlayerPosition;
    private Quaternion savePlayerRotation;

    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private float loadingAnimationTime;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ChangeScene(string nextScene) {
        StartCoroutine(LoadScene(nextScene));
    }

    public IEnumerator LoadScene(string nextScene) {
        if (!nextScene.Equals("Kitchen")) {
            savePlayerPosition = PlayerManager.Instance.transform.position;
            savePlayerRotation = PlayerManager.Instance.transform.rotation;
        }
        
        //Play animation
        loadingScreen.DOFade(1, loadingAnimationTime);
        yield return new WaitForSeconds(loadingAnimationTime);

        if (GameManager.Instance != null) GameManager.Instance.timeElapsing = false;
        
        //Load scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        while (!operation.isDone) yield return null;
        GameManager.Instance.timeElapsing = true;

        if (nextScene.Equals("Kitchen")) {
            PlayerManager.Instance.GetMovement().Teleport(savePlayerPosition,savePlayerRotation);
        }
        
        //End animation
        loadingScreen.DOFade(0, loadingAnimationTime);
    }
}
