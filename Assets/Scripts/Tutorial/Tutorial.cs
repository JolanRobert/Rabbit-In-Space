using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    [SerializeField] private GameObject previousButton, nextButton;
    [SerializeField] private List<GameObject> panels;
    private GameObject currentPanel;
    private int index;

    private void OnEnable() {
        index = 0;
        UpdatePanels();
    }

    public void NextStep() {
        index++;
        UpdatePanels();
    }

    public void PreviousStep() {
        index--;
        UpdatePanels();
    }

    private void UpdatePanels() {
        previousButton.SetActive(index != 0);
        nextButton.SetActive(index != panels.Count - 1);
        
        if(currentPanel != default) currentPanel.SetActive(false);
        currentPanel = panels[index];
        currentPanel.SetActive(true);
    }
}
