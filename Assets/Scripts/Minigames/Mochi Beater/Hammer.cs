using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace MochiBeater
{
    public class Hammer : MonoBehaviour
    {
        [SerializeField] private ErrorCounter errorCounter;
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
                yield return new WaitForSeconds(slamDelay);
                StartCoroutine(Slam());
            }
        }
        IEnumerator Slam()
        {
            animator.SetTrigger("Slam");
            yield return new WaitForSeconds(fromAnimationToSlamDelay);
            if (doughButton.isDragging)
            {
                errorCounter.Fail();
            }
        }
    }
}

