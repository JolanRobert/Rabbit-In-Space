using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour {

    [SerializeField] private CustomerOrder customerOrder;
    [SerializeField] private SpriteRenderer customerSR;

    private CustomerType customerType;
    private int impatienceLimit;

    public void Init(CustomerSO cSo) {
        customerSR.sprite = cSo.customerSprite;
        customerType = cSo.customerType;
        impatienceLimit = cSo.impatienceLimit;
    }

    public void MakeOrder() {
        List<RecipeSO> menu = KitchenManager.Instance.menuGenerator.GetMenu();
        RecipeSO order = null;
        
        switch (customerType) {
            case CustomerType.NORMAL:
                order = menu[Random.Range(0, menu.Count)];
                break;
            case CustomerType.HUPPE:
                break;
            case CustomerType.RADIN:
                break;
            case CustomerType.COPIEUR:
                break;
            case CustomerType.ACCRO:
                break;
            case CustomerType.LENT:
                break;
            case CustomerType.IMPATIENT:
                break;
            case CustomerType.ENERVANT:
                break;
            default:
                throw new Exception("Unknown CustomerType");
        }
        
        customerOrder.gameObject.SetActive(true);
        customerOrder.Init(order);
    }
}