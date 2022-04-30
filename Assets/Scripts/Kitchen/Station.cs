using UnityEngine;

public class Station : IInteractable
{
    [SerializeField] private StationSO station;
    
    public override void Interact() {
        if (RecipeManager.Instance.CheckIsNextStation(station.stationType)) MinigameManager.Instance.StartMinigame(station.minigameScene);
        else PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
