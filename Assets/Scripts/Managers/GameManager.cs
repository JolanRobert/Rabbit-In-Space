using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField] private TMP_Text moneyText;
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

    void Start() {
        moneyText.text = $"{moneyAmount}$";
        xpAmount = 0;
    }

    public void GainGold(int amount) {
        moneyAmount += amount;
        moneyText.text = $"{moneyAmount}$";
    }

    public void GainXP(int amount) {
        if (currentStar.nextStar == null) return;
        
        xpAmount += amount;
        if (xpAmount > currentStar.xpBeforeNextStar) {
            xpAmount -= currentStar.xpBeforeNextStar;
            currentStar = currentStar.nextStar;
        }
    }

    //Return amount of XP before next level
    public int GetXPLeft() {
        return currentStar.xpBeforeNextStar - xpAmount;
    }
}
