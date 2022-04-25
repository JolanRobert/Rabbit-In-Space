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
        int totalTime = time;

        while (time > 0) {
            timeText.text = time.ToString();
            timeBar.DOFillAmount((float) time / totalTime, 1f).SetEase(Ease.Linear);
            yield return oneSec;
            time--;
        }

        EndTimer();
    }

    private void EndTimer() {
        timerGO.SetActive(false);
        if (MinigameManager.Instance.resultPending) MinigameManager.Instance.EndMinigame(false);
        ServiceManager.Instance.EndService();
    }
}
