using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MochiBeater
{
    public class Hammer : MonoBehaviour
    {
        [SerializeField] private ErrorCounter errorCounter;
        [SerializeField] private Image slamGauge;
        [SerializeField] private FoldingDoughButton doughButton;
        [SerializeField] private float slamDelay, fromAnimationToSlamDelay;
        [SerializeField] private Animator animator;
        void Start()
        {
            StartCoroutine(SlamCO());
        }
        IEnumerator SlamCO()
        {
            while (MinigameManager.instance.resultPending)
            {
                slamGauge.DOFillAmount(1, slamDelay + fromAnimationToSlamDelay).SetEase(Ease.Linear);
                yield return new WaitForSeconds(slamDelay);
                animator.SetTrigger("Slam");
                yield return new WaitForSeconds(fromAnimationToSlamDelay);
                slamGauge.fillAmount = 0;
                if (doughButton.isDragging)
                {
                    errorCounter.Fail();
                }
            }
        }
    }
}

