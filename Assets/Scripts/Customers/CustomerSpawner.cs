using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour {

    public static CustomerSpawner Instance;
    
    [Header("Customer")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerSpawnPoint;
    
    [Header("Customer Spawner Values")]
    [SerializeField] private int nbCounterCustomer;
    [SerializeField] private int nbHiddenCustomer;

    public List<Customer> customerQueue = new List<Customer>();

    void Start() {
        Instance = this;
        CheckStarRepartitionValues();
    }

    public void StartService() {
        while (customerQueue.Count < nbCounterCustomer+nbHiddenCustomer) {PopCustomer();}
        MoveCustomers();
    }

    public void EndService() {
        while (customerQueue.Count > 0) DepopCustomer(customerQueue[0]);
    }

    private void PopCustomer() {
        int randomValue = Random.Range(0, 100)+1;
        int value = 0;

        foreach (StarSO.CustomerChance cc in GameManager.Instance.currentStar.customerChances) {
            value += cc.probability;
            if (randomValue > value) continue;
            
            //Handle Copieur xpReward
            if (cc.customerSo.customerType == CustomerType.COPIEUR) {
                if (customerQueue.Count == 0) return;
                cc.customerSo.xpReward = customerQueue[customerQueue.Count - 1].xpReward;
            }
            
            //Spawn Customer
            Customer customer = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.Euler(50,0,0), transform).GetComponent<Customer>();
            
            customer.Init(cc.customerSo);
            customerQueue.Add(customer);
            
            customer.MakeOrder();
            break;
        }
    }

    public void DepopCustomer(Customer customer) {
        customerQueue.Remove(customer);
        customer.CancelOrder();
        Destroy(customer.gameObject);
        
        if (!KitchenManager.Instance.inService) return;
        PopCustomer();
        MoveCustomers();
    }

    //Déplace les clients
    //Gère les commandes et les facteurs d'impatience
    public void MoveCustomers() {
        Vector3 customerOffset = MinigameManager.Instance.resultPending ? Vector3.left * 100 : Vector3.zero;
        
        for (int i = 0; i < customerQueue.Count; i++) {
            customerQueue[i].transform.position = customerSpawnPoint.position + Vector3.right * i * 1f + customerOffset;
            
            //Mise à jour du facteur d'impatience
            int nbEnervants = 0;
            //Client devant
            if (i != 0 && customerQueue[i - 1].myCustomer.customerType == CustomerType.ENERVANT)
                nbEnervants++;
            
            //Client derrière
            if (i != customerQueue.Count-1 && customerQueue[i + 1].myCustomer.customerType == CustomerType.ENERVANT)
                nbEnervants++;

            customerQueue[i].SetImpatienceFactor(1 + 0.25f * nbEnervants);
        }
    }

    private void CheckStarRepartitionValues() {
        int totalProbability = 0;
        List<CustomerSO> clientTypes = new List<CustomerSO>();
        
        foreach (StarSO srSo in DataManager.Instance.starList) {
            foreach (StarSO.CustomerChance cc in srSo.customerChances) {
                if (clientTypes.Contains(cc.customerSo)) {
                    throw new Exception("Plusieurs fois le même type de client - "+srSo.starValue+" étoiles !");
                }

                clientTypes.Add(cc.customerSo);
                totalProbability += cc.probability;
            }

            if (totalProbability != 100) {
                throw new Exception("Probabilité totale égale à "+totalProbability+" % - "+srSo.starValue+" étoiles !");
            }
            
            totalProbability = 0;
            clientTypes.Clear();
            
            //Debug.Log(sr.starValue+" étoiles OK !");
        }
    }
}
