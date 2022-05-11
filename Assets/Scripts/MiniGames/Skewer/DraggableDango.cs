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
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) OnMouseUp();
            else
            {
                rb.velocity = ((camera.ScreenToWorldPoint(touch.position) + Vector3.forward * 10) - (Vector3)rb.position)*7.5f;
                rb.gravityScale = 0;
            }
        }

        if (!isDragged && inTrigger == null) {
            rb.gravityScale = 2;
        }

        //In Skewer block left
        if (inTrigger != null) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.position = new Vector2(Mathf.Max(minPosX, rb.position.x), inTrigger.transform.position.y-0.225f);
            rb.gravityScale = 0;
        }
    }

    private void OnMouseDown() {
        rb.DOKill();
        isDragged = true;
    }

    private void OnMouseUp() {
        isDragged = false;
    }

    public void SetTrigger(SkewerTrigger trigger)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        inTrigger = trigger;

        if (inTrigger == null) minPosX = -100f;
        else minPosX = inTrigger.minPosX +
                       (collider.bounds.extents.x * 2 + 0.1f) * inTrigger.myDangos.Count +
                       collider.bounds.extents.x;
    }
}
}