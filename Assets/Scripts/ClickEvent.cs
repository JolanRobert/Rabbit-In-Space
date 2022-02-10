using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private UnityEvent clickEvent;

    public void OnPointerClick(PointerEventData eventData) {
        clickEvent?.Invoke();
    }
}
