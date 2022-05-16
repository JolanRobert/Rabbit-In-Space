using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField] private Animator rabbitAnimator;
    [SerializeField] private Animator outlineAnimator;

    public bool isAnimateLock;

    public void Speed(float speed) {
        rabbitAnimator.SetFloat("Speed",speed);
        outlineAnimator.SetFloat("Speed",speed);
    }

    public void Haswon(bool state) {
        rabbitAnimator.SetBool("Haswon",state);
        outlineAnimator.SetBool("Haswon",state);

        //Look at Camera
        //if (state) transform.rotation = Quaternion.Euler(0, 170, 0);
        if (state) {
            PlayerManager.Instance.GetMovement().StopMove();
            transform.DORotate(new Vector3(0,170,0),1f);
            isAnimateLock = true;
        }
        else {
            isAnimateLock = false;
        }
    }
}
