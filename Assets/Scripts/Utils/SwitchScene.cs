using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public static SwitchScene Instance;

    private GameManager gameManager;

    void Awake() {
        Instance = this;
    }
    
    public void ToKitchen() {
        StartCoroutine(LoadAsyncScene("Kitchen"));
    }

    public void ToGarden() {
        StartCoroutine(LoadAsyncScene("Garden"));
    }

    public void ToMiniGame(StationType stationType) {
        switch (stationType) {
            case StationType.BOILER:
                StartCoroutine(LoadAsyncScene("Boiler"));
                break;
            case StationType.MOCHI_BEATER:
                StartCoroutine(LoadAsyncScene("Mochi Beater"));
                break;
            case StationType.CUTTER:
                StartCoroutine(LoadAsyncScene("Cutter"));
                break;
            case StationType.SKEWER:
                StartCoroutine(LoadAsyncScene("Skewer"));
                break;
            case StationType.GRILL:
                StartCoroutine(LoadAsyncScene("Grill"));
                break;
            case StationType.TRIMMING:
                StartCoroutine(LoadAsyncScene("Trimming"));
                break;
            case StationType.ROLLING_PIN:
                StartCoroutine(LoadAsyncScene("Rolling Pin"));
                break;
            case StationType.MIXER:
                StartCoroutine(LoadAsyncScene("Mixer"));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stationType), stationType, null);
        }
    }

    private IEnumerator LoadAsyncScene(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone) {
            yield return null;
        }
    }
}
