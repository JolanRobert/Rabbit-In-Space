using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Station", menuName = "ScriptableObjects/Station")]
public class StationSO : ScriptableObject
{
    [Header("Global Infos")]
    public StationType stationType;
    public string minigameScene;

    [Header("Sprites")] public Sprite icon;
}