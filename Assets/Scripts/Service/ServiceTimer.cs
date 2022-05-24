using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServiceTimer : MonoBehaviour {

    public static ServiceTimer Instance;

    [SerializeField] private GameObject timerGO;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image timeBar;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void StartTimer(int time) {
        timerGO.SetActive(true);
        StartCoroutine(TimerRunCoroutine(time));
    }

    private WaitForSeconds oneSec = new WaitForSeconds(1);
    private IEnumerator TimerRunCoroutine(int time) {
        float totalTime = time;
        timeText.text = time.ToString();
        timeBar.DOFillAmount(1, 1f).SetEase(Ease.Linear);
        yield return oneSec;
        
        while (time > 0) {
            while (!GameManager.Instance.timeElapsing) yield return null;
            timeBar.DOFillAmount((time-1)/totalTime, 1).SetEase(Ease.Linear);
            yield return oneSec;

            time--;
            timeText.text = time.ToString();
        }

        EndTimer();
    }

    public void EndTimer() {
        StopAllCoroutines();
        StartCoroutine(TimerEndCoroutine());
    }

    private IEnumerator TimerEndCoroutine() {
        timerGO.SetActive(false);
        if (MinigameManager.Instance.resultPending) MinigameManager.Instance.EndMinigame(false);
        RecipeManager.Instance.EndRecipe(false);

        while (SceneManager.GetActiveScene().name != "Kitchen") yield return null;
        ServiceManager.Instance.EndService();
    }
}
