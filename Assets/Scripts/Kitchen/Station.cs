using UnityEngine;

public class Station : InteractableElement
{
    [SerializeField] private StationSO station;
    public override void Interact() {
        if (RecipeManager.instance.CheckIsNextStation(station.stationType))
        {
            MinigameManager.instance.StartMinigame(station.stationType);
        }
        else
        {
            PlayerManager.Instance.GetInteract().isInteracting = false;
        }
    }
}
