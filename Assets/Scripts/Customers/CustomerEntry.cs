using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerEntry : MonoBehaviour {

    [SerializeField] private TMP_Text customerType;
    [SerializeField] private Image customerImage;
    [SerializeField] private TMP_Text customerBehaviour;
    [SerializeField] private TMP_Text customerXpReward;

    private int nbSeen, nbBeforeReveal;
    private string hiddenBehaviour, revealedBehaviour;
    private string revealedXp;

    public void Init(CustomerSO cSo) {
        name = cSo.name;
        
        customerType.text = cSo.name;
        customerImage.sprite = cSo.customerSprites[0];
        nbBeforeReveal = cSo.nbBeforeReveal;
        revealedBehaviour = cSo.description;
        revealedXp = cSo.customerType == CustomerType.COPIEUR ? "Variable" : cSo.xpReward.ToString();
        
        UpdateReveal(nbBeforeReveal);
    }

    public void UpdateReveal(int amount) {
        nbSeen += amount;
        if (nbSeen >= nbBeforeReveal) {
            customerBehaviour.text = revealedBehaviour;
            customerXpReward.text = "Réputation : " + revealedXp;
        }
        else {
            customerBehaviour.text = "Servez " + nbBeforeReveal + " pour plus d'informations";
            customerXpReward.text = "Réputation : ???";
        }
    }
}
