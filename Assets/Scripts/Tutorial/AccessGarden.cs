using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AccessGarden : TutorialFunc
{
    public static UnityEvent OnOpenGarden = new UnityEvent();
    public override void Do(int index)
    {
        Debug.Log("Function " + index);
        switch (index)
        {
            case 0:
                Welcome();
                break;
            case 1:
                OpenGarden();
                break;
            case 2:
                NextPart();
                break;
        }
    }

    private void Welcome()
    {
        TutorialManager.Instance.OpenPanel(panels[0]);
    }

    private void OpenGarden()
    {
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.OpenPanel(panels[1]);
        OnOpenGarden.AddListener(NextPart);
    }

    
    private void NextPart()
    {
        OnOpenGarden.RemoveListener(NextPart);
        TutorialManager.Instance.ClosePanel();
        TutorialManager.Instance.StartPart(TutorialPart.GROW_RICE);
    }
}
