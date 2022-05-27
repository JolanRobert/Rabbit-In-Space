using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {

    public static UIGame Instance;
    
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private List<Image> starImageList;

    void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGoldText(GameManager.Instance.moneyAmount);
        UpdateStars(GameManager.Instance.currentStar,GameManager.Instance.GetXPLeft());
    }
    
    public void UpdateGoldText(int amount) {
        moneyText.text = $"{amount}$";
    }

    public void UpdateStars(StarSO currentStar, int xpLeft) {
        for (int i = 1; i <= currentStar.starValue; i++) {
            starImageList[i-1].fillAmount = 1;
        }

        if (currentStar.starValue == 5) return;
        starImageList[currentStar.starValue].fillAmount = 1 - xpLeft / (float)currentStar.xpBeforeNextStar;
    }

    public void DontCancelShift() {
        UIManager.Instance.CloseAllPanel();
    }

    public void CancelShift() {
        UIManager.Instance.CloseAllPanel();
        ServiceManager.Instance.LoadMenu();
    }
}
