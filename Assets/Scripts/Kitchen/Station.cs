using UnityEngine;

public class Station : InteractableElement
{
    [SerializeField] private StationSO station;
    public override void Interact() {
        if (RecipeManager.instance.CheckIsNextStation(station.stationType))
        {
            UIManager.Instance.OpenMinigame(interactPanel);
            RecipeManager.instance.ForwardStep();
        }
    }
}
