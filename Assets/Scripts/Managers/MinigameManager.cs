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
        resultPending = true;
    }
    
    public void EndMinigame(bool success) {
        if(!resultPending) return;
        resultPending = false;
        
        SwitchScene.Instance.ChangeScene("Jolan");
        
        KitchenManager.Instance.transform.position += Vector3.right * 100;
        if (success) RecipeManager.Instance.ForwardStep();
    }
}
