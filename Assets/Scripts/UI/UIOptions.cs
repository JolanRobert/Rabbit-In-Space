using UnityEngine;

public class UIOptions : MonoBehaviour {

    [SerializeField] private GameObject optionsMain;
    [SerializeField] private GameObject optionsCheats;
    [SerializeField] private GameObject optionsLeave;
    
    public void MainToCheats() {
        optionsCheats.transform.localScale = Vector3.one;
        optionsMain.SetActive(false);
        optionsCheats.SetActive(true);
    }

    public void CheatsToMain() {
        optionsMain.transform.localScale = Vector3.one;
        optionsMain.SetActive(true);
        optionsCheats.SetActive(false);
    }

    public void MainToLeave() {
        optionsLeave.transform.localScale = Vector3.one;
        optionsLeave.SetActive(true);
    }

    public void LeaveToMain() {
        optionsLeave.SetActive(false);
    }

    public void ExitOptions() {
        if (optionsLeave.activeSelf) UIManager.Instance.ClosePanel(optionsLeave);
        UIManager.Instance.ClosePanel(optionsMain);
        GameManager.Instance.timeElapsing = true;
    }

    public void Quit() {
        Application.Quit();
    }
}
