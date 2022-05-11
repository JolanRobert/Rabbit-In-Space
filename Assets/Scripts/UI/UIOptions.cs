using UnityEngine;

public class UIOptions : MonoBehaviour {

    [SerializeField] private GameObject optionsMain;
    [SerializeField] private GameObject optionsGraphics;
    [SerializeField] private GameObject optionsLeave;
    
    public void MainToGraphics() {
        optionsGraphics.transform.localScale = Vector3.one;
        optionsMain.SetActive(false);
        optionsGraphics.SetActive(true);
    }

    public void GraphicsToMain() {
        optionsMain.transform.localScale = Vector3.one;
        optionsMain.SetActive(true);
        optionsGraphics.SetActive(false);
    }

    public void MainToLeave() {
        optionsLeave.transform.localScale = Vector3.one;
        optionsLeave.SetActive(true);
    }

    public void LeaveToMain() {
        optionsLeave.SetActive(false);
    }

    public void ExitMain() {
        if (optionsLeave.activeSelf) UIManager.Instance.ClosePanel(optionsLeave);
        UIManager.Instance.ClosePanel(optionsMain);
    }

    public void ExitGraphics() {
        UIManager.Instance.ClosePanel(optionsGraphics);
    }

    public void Quit() {
        Application.Quit();
    }
}
