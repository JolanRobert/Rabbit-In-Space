using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomerOrderManager : MonoBehaviour {

    public static CustomerOrderManager Instance;

    public GameObject ordersGO;

    [SerializeField] private RectTransform showPosition;
    [SerializeField] private RectTransform revealPosition;
    [SerializeField] private RectTransform hidePosition;

    [SerializeField] private List<CustomerOrderEntry> customerOrderEntries = new List<CustomerOrderEntry>();
    public List<Customer> orderList = new List<Customer>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void AddCustomerOrder(Customer customer) {
        orderList.Add(customer);
        customerOrderEntries[orderList.Count-1].Init(customer);
        RearrangeOrders();
    }

    private void ShowOrder(CustomerOrderEntry entry) {
        //Transform entryParent = entry.transform.parent;
        entry.DOKill();
        entry.gameObject.SetActive(true);
        entry.transform.DOMoveX(showPosition.position.x, 1);
    }

    private void RevealOrder(CustomerOrderEntry entry) {
        //Transform entryParent = entry.transform.parent;
        entry.DOKill();
        entry.gameObject.SetActive(true);
        entry.transform.DOMoveX(revealPosition.position.x, 1);
    }

    private void HideOrder(CustomerOrderEntry entry) {
        Transform entryParent = entry.transform.parent;
        entry.transform.DOMoveX(hidePosition.position.x, 0);
        entry.gameObject.SetActive(false);

        entryParent.SetAsLastSibling();
        customerOrderEntries.Remove(entry);
        customerOrderEntries.Add(entry);
    }

    private void RearrangeOrders() {
        for (int i = 0; i < orderList.Count; i++) {
            if (i <= 1) {
                ShowOrder(customerOrderEntries[i]);
                orderList[i].Leave();
            }
            else RevealOrder(customerOrderEntries[i]);
        }
    }

    public void RemoveCustomerOrder(Customer customer) {
        int customerIndex = orderList.IndexOf(customer);
        HideOrder(customerOrderEntries[customerIndex]);
        orderList.Remove(customer);
        RearrangeOrders();
        
        if (orderList.Count == 0) ResetOrders();
    }

    private void ResetOrders() {
        for (int i = 0; i < customerOrderEntries.Count; i++) {
            Transform entry = customerOrderEntries[i].transform;
            entry.DOKill();
            entry.DOMoveX(hidePosition.position.x, 0);
        }
    }

    //Change customer head depending of the impatience
    public void UpdateCustomerOrder(Customer customer, Sprite newSprite) {
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if (entry.customer != customer) continue;
            entry.UpdateSprite(newSprite);
            return;
        }
    } 
}
