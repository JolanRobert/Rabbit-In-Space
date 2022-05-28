using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Grill;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrillMochi : MonoBehaviour {

    private SpriteRenderer renderer;

    [SerializeField] private List<Sprite> mochiSprites;
    [SerializeField] private List<GameObject> fxs;

    [SerializeField] private float minGrillSpeedFactor;
    [SerializeField] private float maxgrillSpeedFactor;
    private float grillSpeedFactor;
    private float grillStrenght;
    
    [HideInInspector] public float mochiState = 1f;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        
        grillSpeedFactor = Random.Range(minGrillSpeedFactor, maxgrillSpeedFactor);
        StartCoroutine(Bake());
    }

    private IEnumerator Bake() {
        while (mochiState < 7f) {
            mochiState += grillStrenght * grillSpeedFactor;
            mochiState = Mathf.Clamp(mochiState, 1, 7);
            renderer.sprite = mochiSprites[(int)Mathf.Round(mochiState) - 1];

            if (mochiState < 4) {
                fxs[0].SetActive(true);
                fxs[1].SetActive(false);
                fxs[2].SetActive(false);
            }else if (mochiState < 5) {
                fxs[0].SetActive(false);
                fxs[1].SetActive(true);
                fxs[2].SetActive(false);
            }
            else {
                fxs[0].SetActive(false);
                fxs[1].SetActive(false);
                fxs[2].SetActive(true);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void SetGrillStrenght(float newGrillStrenght) {
        grillStrenght = newGrillStrenght;
    }
    
    private void OnMouseDown() {
        GrillMachine.Instance.GetMochi(this);
        Destroy(gameObject);
    }
}
