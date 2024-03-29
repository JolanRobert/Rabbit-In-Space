using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool timeElapsing = true;

    public int moneyAmount;
    public int xpAmount;
    public StarSO currentStar;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GainGold(int amount) {
        moneyAmount += amount;
        UIGame.Instance.UpdateGoldText(moneyAmount);
    }

    public bool SpendGold(int amount) {
        if (moneyAmount < amount) return false;
        moneyAmount -= amount;
        UIGame.Instance.UpdateGoldText(moneyAmount);
        return true;
    }

    public void GainXP(int amount) {
        if (currentStar.nextStar == null) return;
        
        xpAmount += amount;
        while (xpAmount >= currentStar.xpBeforeNextStar) {
            xpAmount -= currentStar.xpBeforeNextStar;
            currentStar = currentStar.nextStar;
            if (currentStar.nextStar == null) xpAmount = 0;
        }
        
        UIGame.Instance.UpdateStars(currentStar,GetXPLeft());
    }

    //Return amount of XP before next level
    public int GetXPLeft() {
        return currentStar.xpBeforeNextStar - xpAmount;
    }
}
