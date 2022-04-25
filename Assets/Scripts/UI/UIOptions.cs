using UnityEngine;

public class UIOptions : MonoBehaviour {

    [SerializeField] private GameObject optionsMain;
    [SerializeField] private GameObject optionsGraphics;
    [SerializeField] private GameObject optionsLeave;

    public void MainToGraphics() {
        UIManager.Instance.SetVisible(optionsMain,false);
        UIManager.Instance.SetVisible(optionsGraphics,true);
    }

    public void GraphicsToMain() {
        UIManager.Instance.SetVisible(optionsGraphics,false);
        UIManager.Instance.SetVisible(optionsMain,true);
    }

    public void MainToLeave() {
        UIManager.Instance.SetVisible(optionsMain,false);
        UIManager.Instance.SetVisible(optionsLeave,true);
    }

    public void LeaveToMain() {
        UIManager.Instance.SetVisible(optionsLeave,false);
        UIManager.Instance.SetVisible(optionsMain,true);
    }

    public void Quit() {
        Application.Quit();
    }
}
