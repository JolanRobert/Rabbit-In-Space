using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [SerializeField] private List<Parcel> parcels;
    [SerializeField] private List<PlantSO> plantSOs;

    public Parcel currentParcel;
    public Plant currentPlant;

    void Awake() {
        Instance = this;
    }

    public void SelectParcel(Parcel parcel) {
        currentParcel = parcel;
    }

    public void SelectPlant(int plantIndex) {
        currentPlant = currentParcel.plants[plantIndex];
    }

    public void SelectSeed(int seedIndex) {
        currentPlant.PlantSeed(plantSOs[seedIndex]);    
    }
}
