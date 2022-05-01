using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial Part", menuName = "ScriptableObjects/Tutorial Part")]
public class TutorialPartSO : ScriptableObject
{
    public TutorialPart tutorialPart;
    [SerializeField] private List<GameObject> panels;
    public TutorialFunc tutorialFunc;

    public void Begin(){
        tutorialFunc.Do(0);
    }

    public void DoFunc(int index)
    {
        tutorialFunc.Do(index);
    }

    public void SwitchTutorialPart(TutorialPart newTutorialPart)
    {
        tutorialPart = newTutorialPart;
    }
}
