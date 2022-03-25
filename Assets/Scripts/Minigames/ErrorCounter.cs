using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorCounter : MonoBehaviour
{
    [SerializeField] private GameObject errorCrossPrefab;
    private List<GameObject> errorCrosses;
    [SerializeField] private Sprite activeError;
    [SerializeField] private int maxErrors;
    private int currentErrors;
    void Start()
    {
        errorCrosses = new List<GameObject>();
        for (int i = 0; i < maxErrors; i++)
        {
            errorCrosses.Add(Instantiate(errorCrossPrefab, transform));
        }
    }

    public void Fail()
    {
        errorCrosses[currentErrors].GetComponent<Image>().sprite = activeError;
        currentErrors++;
        if(currentErrors == maxErrors) MinigameManager.instance.EndMinigame(false);
    }
}
