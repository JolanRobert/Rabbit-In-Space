using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial Part", menuName = "ScriptableObjects/Tutorial Part")]
public class TutorialPartSO : ScriptableObject
{
    [SerializeField] private List<GameObject> panels;
    public TutorialFunc tutorialFunc;
}
