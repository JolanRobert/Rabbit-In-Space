using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour {

    public static OverlayManager Instance;

    [SerializeField] private Image overlay;
    [SerializeField] private CanvasGroup canvasGroup;

    void Awake() {
        Instance = this;
    }
    
    public void OpenOverlay() {
        overlay.DOComplete();
        canvasGroup.blocksRaycasts = true;
        overlay.DOFade(200 / 255f, 0.325f);
    }

    public void CloseOverlay() {
        overlay.DOComplete();
        overlay.DOFade(0, 0.325f).OnComplete(() => {
            canvasGroup.blocksRaycasts = false;
        });
    }
}
