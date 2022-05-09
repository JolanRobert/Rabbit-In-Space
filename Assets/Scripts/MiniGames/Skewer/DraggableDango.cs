using DG.Tweening;
using UnityEngine;

namespace Skewer {
public class DraggableDango : MonoBehaviour {

    private Camera camera;
    private Rigidbody2D rb;
    private Collider2D collider;

    [Header("Parameters")]
    [SerializeField] private float killY;
    [SerializeField] private float fallSpeed;
    [SerializeField] private Ease easeType;

    private SkewerTrigger inTrigger;
    private float minPosX = -100f;

    private bool isDragged;
    private bool isFalling;

    public DangoColor dangoColor;

    void Awake() {
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public void FocusDango() {
        OnMouseDown();
    }

    void Update() {
        if (isDragged) {
            isFalling = false;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) OnMouseUp();
            else {
                rb.position = camera.ScreenToWorldPoint(touch.position) + Vector3.forward * 10;
            }
        }

        if (!isDragged && !isFalling && inTrigger == null) {
            isFalling = true;
            rb.DOMoveY(killY, fallSpeed).SetSpeedBased(true).SetEase(easeType).OnComplete(() => {
                Destroy(gameObject);
            });
        }

        //In Skewer block left
        if (inTrigger != null) {
            rb.position = new Vector2(Mathf.Max(minPosX, rb.position.x), inTrigger.transform.position.y-0.225f);
        }
    }

    private void OnMouseDown() {
        rb.DOKill();
        isDragged = true;
        rb.mass = 1000000;
    }

    private void OnMouseUp() {
        isDragged = false;
        rb.mass = 1;
    }

    public void SetTrigger(SkewerTrigger trigger) {
        inTrigger = trigger;

        if (inTrigger == null) minPosX = -100f;
        else minPosX = inTrigger.minPosX +
                       (collider.bounds.extents.x * 2 + 0.1f) * inTrigger.myDangos.Count +
                       collider.bounds.extents.x;
    }
}
}