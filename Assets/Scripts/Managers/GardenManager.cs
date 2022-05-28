using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;

    public List<Parcel> parcelList;
    
    [Header("ErrorMsg")]
    [SerializeField] private GameObject errorMsgPrefab;
    private float lastSpawnTime = 0f;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void SelectParcel(GardenEntry entry) {
        myParcel = parcelList[entry.transform.GetSiblingIndex()];
        myParcel.gardenEntry = entry;
    }
    
    //Plant with seed menu
    public void PlantSeed(FoodType itemType) {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            if (foodSo.foodType != itemType) continue;
            myParcel.foodList[mySlot].StartNewFood(foodSo);
            UIGarden.Instance.CloseMenuSeed();
        }
    }

    public void SpawnError(Transform parent, string message) {
        if (Time.time < lastSpawnTime + 1) return;
        ErrorMsg errorMsg = Instantiate(errorMsgPrefab, parent.position, Quaternion.identity, parent).GetComponent<ErrorMsg>();
        errorMsg.Init(message);
        lastSpawnTime = Time.time;
    }
}
