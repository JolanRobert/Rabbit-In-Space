using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour {

    [SerializeField] private int currentStarValue;
    
    [Header("Customer")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerSpawnPoint;
    
    [Header("Customer Spawner Values")]
    public int nbCounterCustomer;
    [SerializeField] private int nbHiddenCustomer;

    public List<Customer> customerQueue = new List<Customer>();

    void Start() {
        CheckStarRepartitionValues();
    }

    public void StartService() {
        while (customerQueue.Count < nbCounterCustomer/*+nbHiddenCustomer*/) {PopCustomer();}
        MoveCustomers();
    }

    public void EndService() {
        while (customerQueue.Count > 0) DepopCustomer(customerQueue[0]);
    }

    private void PopCustomer() {
        int randomValue = Random.Range(0, 100)+1;
        int value = 0;

        foreach (StarRepartitionSO.CustomerChance cc in DataManager.Instance.starRepartitionList[currentStarValue - 1].customerChances) {
            value += cc.probability;
            if (randomValue > value) continue;
            
            //Spawn Customer
            Customer customer = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.Euler(50,0,0), transform).GetComponent<Customer>();
            
            //Handle Copieur xpReward
            if (cc.customerSo.customerType == CustomerType.COPIEUR) {
                if (customerQueue.Count == 0) return;
                cc.customerSo.xpReward = customerQueue[customerQueue.Count - 1].xpReward;
            }
            
            customer.Init(cc.customerSo);
            customerQueue.Add(customer);
            break;
        }
    }

    public void DepopCustomer(Customer customer) {
        customerQueue.Remove(customer);
        Destroy(customer.gameObject);
        
        if (!KitchenManager.Instance.inService) return;
        PopCustomer();
        MoveCustomers();
    }

    //Déplace les clients
    //Gère les commandes et les facteurs d'impatience
    private void MoveCustomers() {
        Vector3 customerOffset = MinigameManager.Instance.resultPending ? Vector3.left * 100 : Vector3.zero;
        
        for (int i = 0; i < customerQueue.Count; i++) {
            customerQueue[i].transform.position = customerSpawnPoint.position - Vector3.right * i * 1.5f + customerOffset;
            customerQueue[i].interactPosition = -customerQueue[i].transform.position + new Vector3(-1.25f, 0, -2);
            
            //Les personnes devant le comptoir passent commande
            if (i < nbCounterCustomer) customerQueue[i].MakeOrder();
            
            //Mise à jour du facteur d'impatience
            int nbEnervants = 0;
            //Client devant
            if (i != 0 && customerQueue[i - 1].customerType == CustomerType.ENERVANT)
                nbEnervants++;
            
            //Client derrière
            if (i != customerQueue.Count-1 && customerQueue[i + 1].customerType == CustomerType.ENERVANT)
                nbEnervants++;

            customerQueue[i].impatienceFactor = 1 + 0.25f * nbEnervants;
        }
    }

    private void CheckStarRepartitionValues() {
        int totalProbability = 0;
        List<CustomerSO> clientTypes = new List<CustomerSO>();
        
        foreach (StarRepartitionSO srSo in DataManager.Instance.starRepartitionList) {
            foreach (StarRepartitionSO.CustomerChance cc in srSo.customerChances) {
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

    /*[Serializable]
    public class StarRepartition {
        public int starValue;
        public List<CustomerChance> customerChances;
    }

    [Serializable]
    public class CustomerChance {
        public CustomerSO customerSo;
        [Range(0,100)] public int probability;
    }*/
}
