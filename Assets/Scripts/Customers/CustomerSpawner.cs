using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour {

    [SerializeField] private int currentStarValue;
    
    [Header("Customer")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerParent;
    
    [Header("Customer Spawner Values")]
    [SerializeField] private int nbCounterCustomer;
    [SerializeField] private int nbHiddenCustomer;
    [SerializeField] private List<StarRepartition> starRepartitions;

    void Start() {
        CheckStarRepartitionValues();
        
        for (int i = 0; i < 1000; i++) SpawnCustomer();
    }

    private void SpawnCustomer() {
        int randomValue = Random.Range(0, 100)+1;
        int value = 0;
        foreach (CustomerChance cc in starRepartitions[currentStarValue - 1].customerChances) {
            value += cc.probability;
            if (randomValue > value) continue;
            
            //Spawn Customer
            GameObject customer = Instantiate(customerPrefab, customerParent);
            customer.GetComponent<Customer>().Init(cc.customerSo);
            break;
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
