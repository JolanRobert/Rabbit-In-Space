using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;
    
    private Vector3 savePlayerPosition;
    private Quaternion savePlayerRotation;

    [SerializeField] private CanvasGroup loadingFade;
    [SerializeField] private VideoPlayer loadingVideo;
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
        loadingVideo.Play();
        loadingFade.DOFade(1, loadingAnimationTime);
        loadingFade.blocksRaycasts = true;
        yield return new WaitForSeconds(loadingAnimationTime);

        if (GameManager.Instance != null) GameManager.Instance.timeElapsing = false;
        
        //Load scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        while (!operation.isDone) yield return null;
        GameManager.Instance.timeElapsing = true;

        if (nextScene.Equals("Kitchen") && savePlayerPosition != default) {
            PlayerManager.Instance.GetMovement().Teleport(savePlayerPosition,savePlayerRotation);
        }
        
        //End animation
        loadingFade.DOFade(0, loadingAnimationTime);
        loadingFade.blocksRaycasts = false;
        yield return new WaitForSeconds(loadingAnimationTime);
        loadingVideo.Stop();
        CustomerSpawner.Instance.MoveCustomers();
    }
}
