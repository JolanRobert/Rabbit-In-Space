using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Station : IInteractable
{
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [SerializeField] private StationSO station;
    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material emptyMaterial;
    public static UnityEvent<StationType> OnStepChange = new UnityEvent<StationType>();

    private void Start()
    { 
        OnStepChange.AddListener(CheckIsSelf);
        glowMaterial.enableInstancing = true;
    }
    public override void Interact() {
        if (RecipeManager.Instance.CheckIsNextStation(station.stationType)) MinigameManager.Instance.StartMinigame(station.minigameScene);
        else PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    private void CheckIsSelf(StationType type) {
        Glow(station.stationType == type);
    }

    List<Material> mats = new List<Material>();
    private void Glow(bool active)
    {
        if (active)
        {
            foreach (MeshRenderer renderer in meshRenderers) {
                renderer.GetMaterials(mats);
                mats[mats.Count - 1] = glowMaterial;
                renderer.materials = mats.ToArray(); //C'est le dernier material qui est changé, faut réserver ce slot vide du coup
            }
            
        }
        else
        {
            foreach (MeshRenderer renderer in meshRenderers) {
                renderer.GetMaterials(mats);
                mats[mats.Count - 1] = emptyMaterial;
                renderer.materials = mats.ToArray();
            }
        }
    }
}
