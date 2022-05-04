using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    
    [Header("UI")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private List<Image> starImageList;

    [Header("Game Values")]
    public int moneyAmount;
    public int xpAmount;
    public StarSO currentStar;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        SetupGameUI();
    }

    private void SetupGameUI() {
        UpdateGoldText(moneyAmount);
        UpdateStars(currentStar,GetXPLeft());
    }

    public void GainGold(int amount) {
        moneyAmount += amount;
        UpdateGoldText(moneyAmount);
    }

    public void GainXP(int amount) {
        if (currentStar.nextStar == null) return;
        
        xpAmount += amount;
        while (xpAmount >= currentStar.xpBeforeNextStar) {
            xpAmount -= currentStar.xpBeforeNextStar;
            currentStar = currentStar.nextStar;
            if (currentStar.nextStar == null) {
                xpAmount = 0;
                return;
            }
        }
        
        UpdateStars(currentStar,GetXPLeft());
    }

    //Return amount of XP before next level
    public int GetXPLeft() {
        return currentStar.xpBeforeNextStar - xpAmount;
    }
    
    //
    //UI
    //
    
    private void UpdateGoldText(int amount) {
        moneyText.text = $"{amount}$";
    }

    private void UpdateStars(StarSO currentStar, int xpLeft) {
        for (int i = 1; i <= currentStar.starValue; i++) {
            starImageList[i-1].fillAmount = 1;
        }

        if (currentStar.starValue == 5) return;
        starImageList[currentStar.starValue].fillAmount = 1 - xpLeft / (float)currentStar.xpBeforeNextStar;
    }
}
