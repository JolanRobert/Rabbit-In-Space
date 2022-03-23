using UnityEngine;

public class MinigameManager : MonoBehaviour {
    
    public static MinigameManager instance;
    
    void Awake() {
        instance = this;
    }

    public void StartMinigame(StationType stationType) {
        SwitchScene.Instance.ToMiniGame(stationType);
        
        //Hide customers
        KitchenManager.Instance.transform.position += Vector3.left*100;
        RecipeManager.instance.HideRecipeTimeline();
    }
    
    public void EndMinigame(bool success) {
        //Show customers
        KitchenManager.Instance.transform.position -= Vector3.left*100;
        
        RecipeManager.instance.ShowRecipeTimeline();
        if (success) RecipeManager.instance.ForwardStep();
    }
}
