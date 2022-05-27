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

    public void Fail() {
        if(currentErrors == maxErrors) return;
        errorCrosses[currentErrors].GetComponent<Image>().color = Color.white;
        currentErrors++;
        if(currentErrors == maxErrors) MinigameManager.Instance.EndMinigame(false);
    }
}
