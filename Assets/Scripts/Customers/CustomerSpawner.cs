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
    [SerializeField] private int nbCounterCustomer;
    [SerializeField] private int nbHiddenCustomer;
    [SerializeField] private List<StarRepartition> starRepartitions;

    public List<Customer> customerQueue = new List<Customer>();

    void Start() {
        CheckStarRepartitionValues();

        KitchenManager.Instance.myMenu.GenerateMenu();
        for (int i = 0; i < nbCounterCustomer+nbHiddenCustomer; i++) PopCustomer();
        MoveCustomers();
    }

    private void PopCustomer() {
        int randomValue = Random.Range(0, 100)+1;
        int value = 0;

        foreach (CustomerChance cc in starRepartitions[currentStarValue - 1].customerChances) {
            value += cc.probability;
            if (randomValue > value) continue;
            
            //Spawn Customer
            Customer customer = Instantiate(customerPrefab, customerSpawnPoint).GetComponent<Customer>();
            customer.Init(cc.customerSo);
            customerQueue.Add(customer);
            break;
        }
    }

    public void DepopCustomer(Customer customer) {
        customerQueue.Remove(customer);
        Destroy(customer.gameObject);
        PopCustomer();
        MoveCustomers();
    }

    //Déplace les clients
    //Gère les commandes et les facteurs d'impatience
    private void MoveCustomers() {
        for (int i = 0; i < customerQueue.Count; i++) {
            customerQueue[i].transform.position = customerSpawnPoint.transform.position - Vector3.right * i * 1.5f;
            
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
        
        foreach (StarRepartition sr in starRepartitions) {
            foreach (CustomerChance cc in sr.customerChances) {
                if (clientTypes.Contains(cc.customerSo)) {
                    throw new Exception("Plusieurs fois le même client dans CustomerSpawner - "+sr.starValue+" étoiles !");
                }

                clientTypes.Add(cc.customerSo);
                totalProbability += cc.probability;
            }

            if (totalProbability != 100) {
                throw new Exception("Probabilité totale égale à "+totalProbability+" % dans CustomerSpawner - "+sr.starValue+" étoiles !");
            }
            
            totalProbability = 0;
            clientTypes.Clear();
            
            //Debug.Log(sr.starValue+" étoiles OK !");
        }
    }

    [Serializable]
    public class StarRepartition {
        public int starValue;
        public List<CustomerChance> customerChances;
    }

    [Serializable]
    public class CustomerChance {
        public CustomerSO customerSo;
        [Range(0,100)] public int probability;
    }
}
