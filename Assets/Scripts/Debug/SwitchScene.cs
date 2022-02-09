using DG.Tweening;
using UnityEngine;

public class SwitchScene : MonoBehaviour {

    private UIManager uiManager;
    private Camera mainCamera;
    private GameManager gameManager;
    private PlayerManager playerManager;

    private void Start() {
        uiManager = UIManager.Instance;
        mainCamera = Camera.main;
        gameManager = GameManager.Instance;
        playerManager = gameManager.playerManager;
    }
    
    public void Switch() {
        uiManager.switchSceneButton.interactable = false;
        if (gameManager.gameState == GameState.KITCHEN) mainCamera.transform.DOMoveX(20, 1.25f).OnComplete(SwitchToGarden);
        else if (gameManager.gameState == GameState.GARDEN) mainCamera.transform.DOMoveX(0, 1.25f).OnComplete(SwitchToKitchen);
    }

    private void SwitchToGarden() {
        playerManager.GetMovement().Teleport(new Vector3(20, 1, -4));
        uiManager.switchSceneText.text = "Switch to Kitchen";
        gameManager.gameState = GameState.GARDEN;
        uiManager.switchSceneButton.interactable = true;
    }

    private void SwitchToKitchen() {
        playerManager.GetMovement().Teleport(new Vector3(0, 1, -4));
        uiManager.switchSceneText.text = "Switch to Garden";
        gameManager.gameState = GameState.KITCHEN;
        uiManager.switchSceneButton.interactable = true;
    }
}
