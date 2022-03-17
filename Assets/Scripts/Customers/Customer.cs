using UnityEngine;

public class Customer : MonoBehaviour {

    [SerializeField] private SpriteRenderer sr;

    private CustomerType customerType;
    private int impatienceLimit;

    public void Init(CustomerSO cSo) {
        sr.sprite = cSo.clientSprite;
        customerType = cSo.customerType;
        impatienceLimit = cSo.impatienceLimit;
    }
}