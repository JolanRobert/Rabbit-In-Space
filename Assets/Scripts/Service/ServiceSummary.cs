using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceSummary : MonoBehaviour {

    [Header("Reputation")]
    [SerializeField] private Image xpBar;
    [SerializeField] private TMP_Text currentStar;
    [SerializeField] private TMP_Text nextStar;
    [SerializeField] private TMP_Text xpLeftText;
    [SerializeField] private TMP_Text todayXPText;

    [Header("Money")]
    [SerializeField] private TMP_Text todayGoldText;

    [Header("Customers")]
    [SerializeField] private TMP_Text servedText;
    [SerializeField] private TMP_Text servedValue;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text leftValue;
    [SerializeField] private TMP_Text kickedText;
    [SerializeField] private TMP_Text kickedValue;

    [Header("Buttons")]
    [SerializeField] private Button nextButton;

    public int todayGold, todayXP;
    private int customersServedAmount, customersLeftAmount, customersKickedAmount;
    private int customerServedXP, customersLeftXP, customersKickedXP;

    private bool isAnimXPEnded, isAnimMoneyEnded, isAnimCustomerEnded;

    public void InitSummary() {
        GameManager gm = GameManager.Instance;
        
        //XP
        UpdateStars(gm.currentStar.starValue,gm.currentStar.nextStar.starValue);
        xpBar.fillAmount = 1 - gm.GetXPLeft() / (float)gm.currentStar.xpBeforeNextStar;
        xpLeftText.text = $"{gm.GetXPLeft()} XP Left";
        todayXPText.text = "+0";
        
        //Money
        todayGoldText.text = "+0";
        
        //Customers
        servedText.text = "Served : 0";
        servedValue.text = "(+0)";
        leftText.text = "Left : 0";
        leftValue.text = "(-0)";
        kickedText.text = "Kicked : 0";
        kickedValue.text = "(-0)";

        nextButton.interactable = false;
    }
    
    public IEnumerator AnimSummary() {
        StartCoroutine(AnimXP(1.5f));
        yield return new WaitForSeconds(0.25f);
        
        StartCoroutine(AnimMoney(1.5f));
        yield return new WaitForSeconds(0.25f);
        
        StartCoroutine(AnimCustomer(servedText, "Served : ", "", 0, customersServedAmount, 0.5f));
        StartCoroutine(AnimCustomer(leftText, "Left : ", "", 0, customersLeftAmount, 0.5f));
        yield return StartCoroutine(AnimCustomer(kickedText, "Kicked : ", "", 0, customersKickedAmount, 0.5f));
        isAnimCustomerEnded = true;
        EndAnim();
        
        servedValue.text = $"(+{customerServedXP})";
        leftValue.text = $"(-{customersLeftXP})";
        kickedValue.text = $"(-{customersKickedXP})";
    }

    private void EndAnim() {
        if (isAnimXPEnded && isAnimMoneyEnded && isAnimCustomerEnded) nextButton.interactable = true;
    }

    private IEnumerator AnimXP(float animTime) {
        if (todayXP < 0) todayXP = 0;
        
        GameManager gm = GameManager.Instance;
        WaitForSeconds stepTime = new WaitForSeconds(animTime / todayXP);

        int tmp_xp = 0;
        while (tmp_xp != todayXP) {
            yield return stepTime;
            tmp_xp++;
            gm.GainXP(1);

            if (gm.currentStar.nextStar == null) xpBar.fillAmount = 1;
            else {
                xpBar.DOFillAmount(1 - gm.GetXPLeft() / (float) gm.currentStar.xpBeforeNextStar, animTime/todayXP).OnComplete(() => {
                    UpdateStars(gm.currentStar.starValue,gm.currentStar.nextStar.starValue);
                });
            }
            
            xpLeftText.text = $"{gm.GetXPLeft()} XP Left";
            todayXPText.text = $"+{tmp_xp}";
        }

        isAnimXPEnded = true;
        EndAnim();
    }

    private IEnumerator AnimMoney(float animTime) {
        GameManager gm = GameManager.Instance;
        WaitForSeconds stepTime = new WaitForSeconds(animTime/todayGold*10);

        int tmp_gold = 0;
        while (tmp_gold < todayGold) {
            yield return stepTime;
            tmp_gold += 10;

            if (tmp_gold > todayGold) {
                gm.GainGold(todayGold%10);
                tmp_gold = todayGold;
            }
            else gm.GainGold(10);

            todayGoldText.text = $"+{tmp_gold}";
        }

        isAnimMoneyEnded = true;
        EndAnim();
    }

    private IEnumerator AnimCustomer(TMP_Text textToAnim, string prefixText, string suffixText, int fromAmount, int toAmount, float animTime) {
        if (fromAmount == toAmount) yield break;
        textToAnim.text = $"{prefixText}{fromAmount}{suffixText}";
        
        WaitForSeconds stepTime = new WaitForSeconds(animTime/(toAmount - fromAmount));
        int newAmount = fromAmount;

        while (newAmount != toAmount) {
            yield return stepTime;
            newAmount++;
            textToAnim.text = $"{prefixText}{newAmount}{suffixText}";
        }
    }
    
    private void UpdateStars(int current, int next) {
        currentStar.text = current.ToString();
        nextStar.text = next.ToString();
    }
    
    public void NewServiceInfo(Customer customer, CustomerState state) {
        switch (state) {
            case CustomerState.SERVED:
                todayGold += customer.myRecipe.goldReward;
                AddCustomerServed(1,customer.xpReward);
                break;
            case CustomerState.LEFT:
                AddCustomerLeft(1,customer.xpReward);
                break;
            case CustomerState.KICKED:
                AddCustomerKicked(1,Mathf.CeilToInt(customer.xpReward*1.5f));
                break;
            default:
                throw new Exception("Unknown customer state");
        }
    }

    private void AddCustomerServed(int amount, int xp) {
        customersServedAmount += amount;
        customerServedXP += xp;
        todayXP += xp;
    }
    
    private void AddCustomerLeft(int amount, int xp) {
        customersLeftAmount += amount;
        customersLeftXP += xp;
        todayXP -= xp;
    }
    
    private void AddCustomerKicked(int amount, int xp) {
        customersKickedAmount += amount;
        customersKickedXP += xp;
        todayXP -= xp;
    }

    public void ResetSummary() {
        todayXP = 0;
        todayGold = 0;
        customersServedAmount = 0;
        customersLeftAmount = 0;
        customersKickedAmount = 0;
        customerServedXP = 0;
        customersLeftXP = 0;
        customersKickedXP = 0;
    }
}
