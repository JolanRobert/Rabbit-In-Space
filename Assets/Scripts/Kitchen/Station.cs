using UnityEngine;
using UnityEngine.SceneManagement;

public class Station : IInteractable
{
    [SerializeField] private StationSO station;
    
    public override void Interact() {
        if (RecipeManager.Instance.CheckIsNextStation(station.stationType)) MinigameManager.Instance.StartMinigame(station.minigameScene.name);
        else PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
