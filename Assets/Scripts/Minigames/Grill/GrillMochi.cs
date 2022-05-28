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
