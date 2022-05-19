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
        timeText.text = time.ToString();
        timeBar.DOFillAmount(1, 1f).SetEase(Ease.Linear);
        yield return oneSec;
        timeBar.DOFillAmount(0, time).SetEase(Ease.Linear);

        while (time > 0)
        {
            yield return oneSec;
            if (!GameManager.Instance.timeElapsing) continue;
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
