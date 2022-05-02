using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial Part", menuName = "ScriptableObjects/Tutorial Part")]
public class TutorialPartSO : ScriptableObject
{
    public TutorialPart tutorialPart;
    [SerializeField] public List<GameObject> panels;
    public TutorialFunc tutorialFunc;

    public void Setup()
    {
        switch (tutorialPart)
        {
            case TutorialPart.ACCESS_GARDEN:
                tutorialFunc = new AccessGarden();
                break;
        }
    }
    public void Begin(){
        tutorialFunc.Do(0);
    }

    public void DoFunc(int index)
    {
        tutorialFunc.Do(index);
    }
}