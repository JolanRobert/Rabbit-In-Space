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

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    private void ShowOrder(CustomerOrderEntry entry) {
        entry.DOKill();
        entry.gameObject.SetActive(true);
        entry.transform.DOMoveX(showPosition.position.x, 1);
    }

    private void RevealOrder(CustomerOrderEntry entry) {
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
        for (int i = 0; i < customerOrderEntries.Count; i++) {
            if (i <= 1) {
                ShowOrder(customerOrderEntries[i]);
                if (customerOrderEntries[i].customer != null) customerOrderEntries[i].customer.Leave();
            }
            else RevealOrder(customerOrderEntries[i]);
        }
        UpdateCustomersOwnedRecipeAmount();
    }
    
    public void AddCustomerOrder(Customer customer) {
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if (entry.customer != null) continue;
            entry.Init(customer);
            break;
        }
        
        RearrangeOrders();
    }

    public void RemoveCustomerOrder(Customer customer) {
        CustomerOrderEntry entry = GetCustomerOrderEntry(customer);
        entry.ResetBackground();
        entry.customer = null;
        
        HideOrder(entry);
    }

    //Change customer head depending of the impatience
    public void UpdateCustomerOrder(Customer customer, Sprite newSprite) {
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if (entry.customer != customer) continue;
            entry.UpdateSprite(newSprite);
            return;
        }
    }

    //Change customer impatience background speed, if impatience factor is updated
    public void UpdateCustomerOrder(Customer customer, float timeLeft, float impatienceFactor) {
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if (entry.customer != customer) continue;
            entry.UpdateBackground(timeLeft,impatienceFactor);
            return;
        }
    }

    private CustomerOrderEntry GetCustomerOrderEntry(Customer customer) {
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if (entry.customer == customer) return entry;
        }

        return null;
    }

    public void UpdateCustomersOwnedRecipeAmount() {
        if(customerOrderEntries == null) return;
        foreach (CustomerOrderEntry entry in customerOrderEntries) {
            if(entry.customer == null) continue;
            entry.UpdateOwnedRecipeAmount();
        }
    }

    public void ForceServe() {
        customerOrderEntries[0].ForceServe();
    }
}
