using UnityEngine;

public class Station : InteractableElement
{
    [SerializeField] private StationSO station;
    public override void Interact() {
        if (RecipeManager.instance.CheckIsNextStation(station.stationType))
        {
            UIManager.Instance.OpenMinigame(interactPanel.gameObject);
            RecipeManager.instance.ForwardStep();
        }
        else
        {
            PlayerManager.Instance.GetInteract().isInteracting = false;
        }
    }
}
