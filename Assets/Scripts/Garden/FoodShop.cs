using System.Collections.Generic;
using UnityEngine;

public class FoodShop : MonoBehaviour {
    
    [SerializeField] private List<FoodSO> foodSos;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private Transform contentParent;
    
    void Start() {
        foreach (FoodSO foodSo in foodSos) {
            GameObject entry = Instantiate(entryPrefab, contentParent);
            entry.GetComponent<FoodShopEntry>().Init(foodSo);
        }
    }
}
