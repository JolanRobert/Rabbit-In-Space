using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public List<TutorialPartSO> tutorialPartsSO;
    public TutorialPartSO currentTutorialPart;
    public TutorialFunc currentTutorialFunc;
    [SerializeField] private Transform canvasTransform;
    private GameObject currentPanel;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Check if tutorial is already done
        StartPart(TutorialPart.ACCESS_GARDEN);
    }

    public void StartPart(TutorialPart tutorialPart)
    {
        foreach (TutorialPartSO part in tutorialPartsSO)
        {
            if (part.tutorialPart == tutorialPart)
            {
                Debug.Log("Starting " + part.tutorialPart);
                currentTutorialPart = part;
                part.Setup();
                currentTutorialPart.tutorialFunc.Setup(currentTutorialPart.panels);
                part.Begin();
                return;
            }
        }
        Debug.Log("Tutorial part was not found");
    }

    public void OpenPanel(GameObject panel)
    {
        currentPanel = Instantiate(panel, canvasTransform);
    }

    public void ClosePanel()
    {
        Destroy(currentPanel);
        currentPanel = null;
    }
}
