using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MochiBeater
{
    public class MochiBeaterManager : MonoBehaviour
    {
        public static MochiBeaterManager Instance;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform arrow, dough;
        [SerializeField] private GameObject foldingButton;
        [SerializeField] private int foldsToMake;
        [Range(0f,1f)][SerializeField] private float newFoldProbability;
        private Vector2 foldingVector, foldingDir;
        [SerializeField] private float foldingMagnitude;
        [SerializeField] private float dirMargin, magnitudeMargin;

        void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            GetNewFolding();
        }

        void GetNewFolding()
        {
            foldingDir = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
            foldingVector = foldingDir * foldingMagnitude; 
            arrow.eulerAngles = new Vector3 (0, 0, Vector2.SignedAngle(transform.right, foldingVector));
            dough.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(transform.up, foldingVector));
            foldingButton.transform.position = Camera.main.WorldToScreenPoint(-foldingVector / 2);
        }

        public void TryFold(Vector2 drag)
        {
            Debug.Log(drag.magnitude + ", " + drag.normalized);
            if (!(drag.magnitude > foldingMagnitude - magnitudeMargin / 2) || !(drag.magnitude < foldingMagnitude + magnitudeMargin / 2))
            {
                Debug.Log("Invalid Magnitude");
                return;
            }
            if (!(drag.normalized.x > foldingDir.x - dirMargin / 2) ||
                !(drag.normalized.x < foldingDir.x + dirMargin / 2) ||
                !(drag.normalized.y > foldingDir.y - dirMargin / 2) ||
                !(drag.normalized.y < foldingDir.y + dirMargin / 2))
            {
                Debug.Log("Invalid dir");
                return;
            }
            SucceedFold();
        }

        private void SucceedFold()
        {
            animator.SetTrigger("FoldSuccess");
            foldsToMake--;
            if (foldsToMake <= 0)
            {
                MinigameManager.instance.EndMinigame(true);
                return;
            }
            if(Random.Range(0f,1f) < newFoldProbability) GetNewFolding();
        }
    }
}