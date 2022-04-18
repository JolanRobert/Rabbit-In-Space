using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StarRepartition", menuName = "ScriptableObjects/StarRepartition")]
public class StarRepartitionSO : ScriptableObject {

    public int starValue;
    public List<CustomerChance> customerChances;

    [Serializable]
    public class CustomerChance {
        public CustomerSO customerSo;
        [Range(0,100)] public int probability;
    }
}