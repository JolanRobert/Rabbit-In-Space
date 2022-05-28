using DG.Tweening;
using TMPro;
using UnityEngine;

public class ErrorMsg : MonoBehaviour {

    [SerializeField] private TMP_Text msg;
    
    public void Init(string message) {
        msg.text = message;
        Anim();
    }

    private void Anim() {
        transform.DOMoveY(transform.position.y + 50, 1f).SetEase(Ease.Linear);
        msg.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {
            Destroy(this);
        });
    }
}
