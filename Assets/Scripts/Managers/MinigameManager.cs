using UnityEngine;

public class MinigameManager : MonoBehaviour {
    
    public static MinigameManager Instance;
    public bool resultPending;
    
    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void StartMinigame(string minigameScene) {
        SwitchScene.Instance.ChangeScene(minigameScene);
        
        //Hide customers
        KitchenManager.Instance.transform.position += Vector3.left*100;
        RecipeManager.Instance.HideRecipeTimeline();
        resultPending = true;
    }
    
    public void EndMinigame(bool success) {
        if(!resultPending) return;
        resultPending = false;
        
        SwitchScene.Instance.ChangeScene("Kitchen");
        
        KitchenManager.Instance.transform.position += Vector3.right * 100;
        RecipeManager.Instance.ShowRecipeTimeline();
        if (success) RecipeManager.Instance.ForwardStep();
    }
}
