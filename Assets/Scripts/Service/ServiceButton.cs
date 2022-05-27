using System;
using UnityEngine;

public class ServiceButton : IInteractable {
    public static ServiceButton Instance;
    private MeshRenderer renderer;
    [SerializeField] private Material openMaterial, closeMaterial;
    void Start() {
        Instance = this;
        renderer = GetComponent<MeshRenderer>();
        ServiceManager.Instance.UpdateWindow();
        UpdateMaterial();
    }
    
    public override void Interact() {
        ServiceManager.Instance.LoadMenuFromButton();
    }

    public void UpdateMaterial() {
        renderer.material = KitchenManager.Instance.inService ? closeMaterial : openMaterial;
    }
}
