using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Station", menuName = "ScriptableObjects/Stations")]
public class StationSO : ScriptableObject
{
    [Header("Global Infos")]
    public new string name;
    public StationType stationType;

    [Header("Sprites")] public Sprite icon;
}