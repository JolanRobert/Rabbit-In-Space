using System.Collections.Generic;
using UnityEngine;

public class ParcelUpgrade : MonoBehaviour {

    [SerializeField] private List<ParcelUpgradeSO> puSos;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private Transform contentParent;

    void Start() {
        foreach (ParcelUpgradeSO puSo in puSos) {
            GameObject entry = Instantiate(entryPrefab, contentParent);
            entry.GetComponent<ParcelUpgradeEntry>().Init(puSo);
        }
    }
}
