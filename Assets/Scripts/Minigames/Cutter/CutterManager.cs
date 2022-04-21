using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Cutter
{
    public class CutterManager : MonoBehaviour
    {
        public static CutterManager instance;
        [SerializeField] private ErrorCounter errorCounter;
        [SerializeField] private GameObject doughPrefab;
        [SerializeField] private Image completionGauge;
        [SerializeField] private int initialAmountToCut;
        [SerializeField] private float delayBetweenThrows;
        [SerializeField] private float doughThrowStrengthMin, doughThrowStrengthMax, doughThrowStrength;
        [SerializeField] private float throwSwingMax;
        [SerializeField] public float startPosY, startPosXMin, startPosXMax;
        private GameObject dough;
        private int amountToCut;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            amountToCut = initialAmountToCut;
            StartCoroutine(ThrowDoughCO());
        }

        IEnumerator ThrowDoughCO()
        {
            while (MinigameManager.Instance.resultPending)
            {
                yield return new WaitForSeconds(delayBetweenThrows);
                ThrowDough();
            }
        }
        private float throwSwing;
        void ThrowDough()
        {
            doughThrowStrength = Random.Range(doughThrowStrengthMin, doughThrowStrengthMax);
            throwSwing = Random.Range(-throwSwingMax, throwSwingMax);
            
            dough = Instantiate(doughPrefab, new Vector3(Random.Range(startPosXMin, startPosXMax),startPosY,0), 
                Quaternion.Euler(0,0,Random.Range(0f,360f)), transform);
            dough.GetComponent<Rigidbody>().AddForce(new Vector2(throwSwing*doughThrowStrength, (1-Math.Abs(throwSwing))*doughThrowStrength));
        }

        public void SucceedSlice()
        {
            amountToCut--;
            completionGauge.fillAmount += 1f / initialAmountToCut;
            if (amountToCut <= 0)
            {
                MinigameManager.Instance.EndMinigame(true);
            }
        }
        public void Fail()
        {
            errorCounter.Fail();
        }
    }
}
