using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    private Vector3 initialPosition;
    private float initialXRotation;
    
    [Header("Summary Focus")]
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private float newXRotation;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        initialPosition = transform.position;
        initialXRotation = transform.eulerAngles.x;
    }

    public void FocusElement(Transform target) {
        transform.DOMove(target.position + offsetPosition, 1f);
        transform.DORotate(new Vector3(newXRotation,transform.eulerAngles.y,transform.eulerAngles.z), 1f);
    }

    public void Reset() {
        transform.DOMove(initialPosition, 1f);
        transform.DORotate(new Vector3(initialXRotation,transform.eulerAngles.y,transform.eulerAngles.z),1f);
    }
}
