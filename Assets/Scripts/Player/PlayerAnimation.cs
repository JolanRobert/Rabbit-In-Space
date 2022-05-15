using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField] private Animator rabbitAnimator;
    [SerializeField] private Animator outlineAnimator;

    public void Speed(float speed) {
        rabbitAnimator.SetFloat("Speed",speed);
        outlineAnimator.SetFloat("Speed",speed);
    }

    public void Haswon(bool state) {
        rabbitAnimator.SetBool("Haswon",state);
        outlineAnimator.SetBool("Haswon",state);
    }
}
