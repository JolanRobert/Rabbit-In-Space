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
            case TutorialPart.GROW_RICE:
                tutorialFunc = new GrowRice();
                break;
            case TutorialPart.START_SERVICE:
                //tutorialFunc = new StartService();
                break;
            case TutorialPart.START_RECIPE:
                //tutorialFunc = new StartRecipe();
                break;
            case TutorialPart.START_MINI_GAME:
                //tutorialFunc = new StartMiniGame();
                break;
            case TutorialPart.SERVE:
                //tutorialFunc = new Serve();
                break;
            case TutorialPart.END_SERVICE:
                //tutorialFunc = new EndService();
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