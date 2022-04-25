using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderManager : MonoBehaviour {

    public static CustomerOrderManager Instance;
    
    [SerializeField] private List<RectTransform> customerOrderEntries = new List<RectTransform>();
    private List<Customer> orderList = new List<Customer>();

    void Awake() {
        Instance = this;
    }

    public void AddCustomerOrder(Customer customer) {
        orderList.Add(customer);
        customerOrderEntries[orderList.Count-1].GetComponent<CustomerOrderEntry>().Init(customer);
        RearrangeOrders();
    }

    public void ShowOrder(int index) {
        
    }

    public void RevealOrder(int index) {
        
    }

    public void HideOrder(int index) {
        
    }

    private void RearrangeOrders() {
        
    }

    public void RemoveCustomerOrder(Customer customer) {
        orderList.Remove(customer);
        RearrangeOrders();
    }
    
}
