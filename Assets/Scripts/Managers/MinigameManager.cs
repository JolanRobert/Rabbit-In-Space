using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour {
    
    public static MinigameManager Instance;
    [HideInInspector]
    public bool resultPending = true;
    
    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void StartMinigame(string minigameScene) {
        SwitchScene.Instance.ChangeScene(minigameScene);
        
        //Hide customers
        KitchenManager.Instance.transform.position += Vector3.left*100;
        resultPending = true;
        
        //Hide orders
        CustomerOrderManager.Instance.ordersGO.SetActive(false);
    }
    
    public void EndMinigame(bool success) {
        if(!resultPending) return;
        resultPending = false;
        StartCoroutine(EndMinigameCR(success));
    }
    
    private IEnumerator EndMinigameCR(bool success) {
        yield return SwitchScene.Instance.LoadScene("Kitchen");
        
        //Show customers and orders
        KitchenManager.Instance.transform.position += Vector3.right * 100;
        CustomerOrderManager.Instance.ordersGO.SetActive(true);
        
        if (success) RecipeManager.Instance.ForwardStep();
        else StartCoroutine(RecipeManager.Instance.WaitForGlow());
    }
}
