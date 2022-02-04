using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private Camera mainCamera;
    private GameManager gameManager;
    private PlayerManager playerManager;
    
    [SerializeField] private Button switchSceneButton;
    private TMP_Text switchSceneText;

    private void Start() {
        mainCamera = Camera.main;
        gameManager = GameManager.instance;
        playerManager = gameManager.playerManager;

        switchSceneText = switchSceneButton.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void SwitchScene() {
        switchSceneButton.interactable = false;
        if (gameManager.gameState == GameState.KITCHEN) mainCamera.transform.DOMoveX(20, 1.25f).OnComplete(SwitchToGarden);
        else if (gameManager.gameState == GameState.GARDEN) mainCamera.transform.DOMoveX(0, 1.25f).OnComplete(SwitchToKitchen);
    }

    private void SwitchToGarden() {
        playerManager.GetMovement().Teleport(new Vector3(20, 1, -4));
        switchSceneText.text = "Switch to Kitchen";
        gameManager.gameState = GameState.GARDEN;
        switchSceneButton.interactable = true;
    }

    private void SwitchToKitchen() {
        playerManager.GetMovement().Teleport(new Vector3(0, 1, -4));
        switchSceneText.text = "Switch to Garden";
        gameManager.gameState = GameState.KITCHEN;
        switchSceneButton.interactable = true;
    }
}
