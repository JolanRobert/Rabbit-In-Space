using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Grill {
public class CompletionBar : MonoBehaviour {
    
    private Image completionBar;
    private float completionValue;
    
    void Start() {
        completionBar = GetComponent<Image>();
    }

    public void FillBar(int amount) {
        completionValue += amount;
        completionBar.DOFillAmount(completionValue/100, 0.2f);

        if (completionValue >= 100) {
            MinigameManager.Instance.EndMinigame(true);
        }
    }
}
}

