using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public List<TutorialPartSO> tutorialPartsSO;
    public TutorialPartSO currentTutorialPart;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Check if tutorial is already done
    }

    public void StartPart(TutorialPart tutorialPart)
    {
        
    }
}
