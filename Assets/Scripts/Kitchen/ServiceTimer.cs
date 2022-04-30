using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
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
        StartCoroutine(TimerCoroutine(time));
    }

    private WaitForSeconds oneSec = new WaitForSeconds(1);
    private IEnumerator TimerCoroutine(int time) {
        timeText.text = time.ToString();
        timeBar.DOFillAmount(1, 1f).SetEase(Ease.Linear);
        yield return oneSec;
        timeBar.DOFillAmount(0, time).SetEase(Ease.Linear);

        while (time > 0) {
            yield return oneSec;
            time--;
            timeText.text = time.ToString();
        }

        EndTimer();
    }

    private void EndTimer() {
        timerGO.SetActive(false);
        if (MinigameManager.Instance.resultPending) MinigameManager.Instance.EndMinigame(false);
        RecipeManager.Instance.EndRecipe(false);
        ServiceManager.Instance.EndService();
    }
}
