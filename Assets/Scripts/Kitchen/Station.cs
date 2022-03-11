using UnityEngine;

public class Station : InteractableElement
{
    [SerializeField] private StationSO station;
    public override void Interact() {
        UIManager.Instance.OpenMinigame(interactPanel);
    }
}
