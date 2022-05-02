using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrowRice : TutorialFunc
{
    public static UnityEvent OnOpenParcel = new UnityEvent();
    public static UnityEvent OnOpenSeedMenu = new UnityEvent();
    public static UnityEvent OnPlantMoonRice = new UnityEvent();
    public static UnityEvent OnCollectMoonRice = new UnityEvent();
    public override void Do(int index)
    {
        Debug.Log("Function " + index);
        switch (index)
        {
            case 0:
                Presentation();
                break;
            case 1:
                NowClickAPlot();
                break;
            case 2:
                LetsPlantRice();
                break;
            case 3:
                RiceIsFree();
                break;
            case 4:
                CropsTakeTime();
                break;
            case 5:
                OnceMature();
                break;
            case 6:
                DecayWarning();
                break;
            case 7:
                ReturnToKitchen();
                break;
            case 8:
                NextStep();
                break;
        }
    }

    private void Presentation()
    {
        TutorialManager.Instance.OpenPanel(panels[0]);
        OnOpenParcel.AddListener(ThisIsAPlot);
    }

    private void NowClickAPlot()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[1]);
    }

    private void ThisIsAPlot()
    {
        OnOpenParcel.RemoveAllListeners();
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[2]);
    }

    private void LetsPlantRice()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[3]);
        OnOpenSeedMenu.AddListener(PointRice);
    }

    private void PointRice()
    {
        OnOpenParcel.RemoveAllListeners();
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[4]);
        OnPlantMoonRice.AddListener(RiceIsGrowing);
    }

    private void RiceIsGrowing()
    {
        OnPlantMoonRice.RemoveAllListeners();
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[5]);
    }

    private void RiceIsFree()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[6]);
    }
    
    private void CropsTakeTime()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[7]);
    }

    private void OnceMature()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[8]);
    }

    private void DecayWarning()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[9]);
        OnCollectMoonRice.AddListener(Congrats);
    }

    private void Congrats()
    {
        OnCollectMoonRice.RemoveAllListeners();
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[10]);
        FoodDataManager.Instance.AddItem(FoodType.MOON_RICE, 10);
    }

    private void ReturnToKitchen()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[11]);
    }

    private void NextStep()
    {
        Debug.Log("Next step");
        TutorialManager.Instance.ClosePanel();
        UIManager.Instance.CloseAllPanel();
    }
}
