using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Skewer {
public class DraggableDango : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    private Collider2D collider;

    [SerializeField] private float killY;
    [SerializeField] private float fallSpeed;

    public SkewerTrigger inTrigger;
    [SerializeField] private Ease easeType;

    private Vector2 screenPosition;
    private int touchId;

    private bool isDragged;
    private bool isFalling;

    public DangoColor dangoColor;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    public void FocusDango() {
        OnMouseDown();
    }

    void Update() {
        if (isDragged) {
            isFalling = false;
            var touch = Input.touches.FirstOrDefault(t => t.fingerId == touchId);

            if (touch.phase == TouchPhase.Ended) OnMouseUp();
            else {
                screenPosition = touch.position;
                rb.position = Camera.main.ScreenToWorldPoint(screenPosition) + Vector3.forward * 10;
            }
        }

        if (!isDragged && !isFalling && inTrigger == null) {
            isFalling = true;
            rb.DOMoveY(killY, fallSpeed).SetSpeedBased(true).SetEase(easeType)
                .OnComplete(() => { Destroy(gameObject); });
        }

        //In Skewer block left
        if (inTrigger != null) {
            float minX = (inTrigger.collider.bounds.center - inTrigger.collider.bounds.extents + collider.bounds.extents).x;
            rb.position = new Vector2(Mathf.Max(minX, rb.position.x), inTrigger.transform.position.y);
        }
    }

    private void OnMouseDown() {
        rb.DOKill();
        isDragged = true;
        touchId = Input.touches.FirstOrDefault(t => t.phase == TouchPhase.Began).fingerId;
    }

    private void OnMouseUp() {
        isDragged = false;
    }
}
}