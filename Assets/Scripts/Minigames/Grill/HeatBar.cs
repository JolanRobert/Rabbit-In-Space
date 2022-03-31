using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Grill {
public class HeatBar : MonoBehaviour {

    private Image heatBar;
    
    [Range(1,100)] [SerializeField] private int coolingPercentPerSec;
    public float heatValue;
    
    void Start() {
        heatBar = GetComponent<Image>();
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown() {
        while (true) {
            ChangeHeat(-coolingPercentPerSec);
            yield return new WaitForSeconds(1);
        }
    }

    public void ChangeHeat(int amount) {
        heatValue = Mathf.Clamp(heatValue + amount, 0, 100);
        heatBar.DOFillAmount(heatValue/100, 0.2f);
    }
}
}

