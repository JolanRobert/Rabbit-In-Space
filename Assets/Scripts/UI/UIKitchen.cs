using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIKitchen : MonoBehaviour {

    public static UIKitchen Instance;

    public GameObject recipeOverridePrompt;
    public GameObject recipeAmountPrompt;
    
    public List<InventorySlot> fridgeSlots = new List<InventorySlot>();
    public List<InventorySlot> workplanSlots = new List<InventorySlot>();

    [Header("Fridge")]
    [SerializeField] private GameObject fridge;
    [SerializeField] private Image fridgeDoor;
    private bool isFridgeOpen;

    void Awake() {
        Instance = this;
    }

    public void OpenFridge() {
        if (isFridgeOpen) return;
        StartCoroutine(OpenFridgeCR());
    }

    private IEnumerator OpenFridgeCR() {
        isFridgeOpen = true;
        UIManager.Instance.OpenPanel(fridge);
        
        yield return new WaitForSeconds(0.325f);
        
        fridgeDoor.DOComplete();
        fridgeDoor.DOFade(0, 0.325f).OnComplete(() => {
            fridgeDoor.gameObject.SetActive(false);
        });
    }

    public void CloseFridge() {
        fridgeDoor.DOComplete();
        fridgeDoor.gameObject.SetActive(true);
        fridgeDoor.DOFade(1, 0.325f).OnComplete(() => {
            UIManager.Instance.ClosePanel(fridge);
            isFridgeOpen = false;
        });
    }

    public void UpdateFridgeSlot(int slotIndex, int value) {
        PrefabManager.Instance.SpawnPrefabs();
        fridgeSlots[slotIndex].UpdateAmount(value);
    }
    
    public void UpdateWorkplanSlot(int slotIndex, int value) {
        workplanSlots[slotIndex].UpdateAmount(value);
    }
    
    public void OnClickConfirmOverrideRecipe() {
        RecipeManager.Instance.EndRecipe(false);
        RecipeManager.Instance.PromptAmountRecipe();
    }
}
