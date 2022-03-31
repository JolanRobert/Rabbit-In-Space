using System.Collections;
using DG.Tweening;
using Grill;
using UnityEngine;

public class GrillMochi : MonoBehaviour {

    private SpriteRenderer mochiSprite;
    
    [SerializeField] private float minGrillSpeedFactor;
    [SerializeField] private float maxgrillSpeedFactor;
    private float grillSpeedFactor;
    private float grillStrenght;
    
    [HideInInspector] public float mochiState = 1f;

    void Start() {
        mochiSprite = GetComponent<SpriteRenderer>();
        
        grillSpeedFactor = Random.Range(minGrillSpeedFactor, maxgrillSpeedFactor);
        StartCoroutine(Bake());
    }

    private IEnumerator Bake() {
        while (mochiState < 7f) {
            mochiState += grillStrenght * grillSpeedFactor;
            mochiState = Mathf.Clamp(mochiState, 1, 7);
            mochiSprite.DOColor(
                mochiState <= 4
                    ? new Color(1, 1, 1 - (mochiState - 1) / 3, 1)
                    : new Color(1 - (mochiState - 4) / 3, 1 - (mochiState - 4) / 3, 0, 1), 0.2f);

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
