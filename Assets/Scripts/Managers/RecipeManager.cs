using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    private Queue<StationType> stations = new Queue<StationType>();

    void Awake()
    {
        instance = this;
    }

    public void StartRecipeTimeline(List<StationSO> newStations)
    {
        stations.Clear();
        foreach (StationSO station in newStations)
        {
            stations.Enqueue(station.stationType);
        }
    }
}
