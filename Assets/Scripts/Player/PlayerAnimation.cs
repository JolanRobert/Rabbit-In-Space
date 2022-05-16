using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField] private Animator rabbitAnimator;
    [SerializeField] private Animator outlineAnimator;

    public bool isLock;

    public void Speed(float speed) {
        rabbitAnimator.SetFloat("Speed",speed);
        outlineAnimator.SetFloat("Speed",speed);
    }

    public void Haswon(bool state) {
        //Look at Camera
        if (state) {
            rabbitAnimator.Play("Win");
            outlineAnimator.Play("Win");
            PlayerManager.Instance.GetMovement().StopMove();
            transform.DORotate(new Vector3(0,170,0),1f);
            isLock = true;
        }
        else {
            rabbitAnimator.Play("Idle");
            outlineAnimator.Play("Idle");
            isLock = false;
        }
    }
}
