using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;
    private Vector3 playerPos;
    private Quaternion playerRotation;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    public void ChangeScene(string newScene)
    {
        if (SceneManager.GetActiveScene().name == "Kitchen")
        {
            playerPos = PlayerManager.Instance.transform.position;
            playerRotation = PlayerManager.Instance.transform.rotation;
        }
        StartCoroutine(LoadAsyncScene(newScene));
    }

    private IEnumerator LoadAsyncScene(string nextScene) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone) {
            yield return null;
        }
        
        if (nextScene == "Kitchen" && playerPos != default)
        {
            PlayerManager.Instance.GetMovement().Teleport(playerPos);
            PlayerManager.Instance.transform.rotation = playerRotation;
        }
    }
}
