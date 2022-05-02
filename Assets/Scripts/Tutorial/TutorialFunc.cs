using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialFunc
{
    protected List<GameObject> panels;
    public void Setup(List<GameObject> newPanels)
    {
        panels = newPanels;
    }

    public abstract void Do(int index);
}
