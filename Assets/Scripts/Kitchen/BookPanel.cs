using UnityEngine;

public class BookPanel : MonoBehaviour {
    
    [SerializeField] private Transform panelGroup;
    [SerializeField] private GameObject recipePanelPrefab;
    private GameObject recipePanel;
    
    void Start() {
        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            recipePanel = Instantiate(recipePanelPrefab, Vector3.zero, Quaternion.identity, panelGroup);
            recipePanel.GetComponent<RecipePanel>().SetupPanel(rSo);
        }
    }
}
