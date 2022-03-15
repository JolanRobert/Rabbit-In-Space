using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;
    [SerializeField] private Transform miniGamePos;
    [SerializeField] private List<MiniGame> miniGames;
    private GameObject miniGameGO;
    private int fails;

    void Awake()
    {
        instance = this;
    }

    public void StartMinigame(StationType stationType)
    {
        fails = 0;
        foreach (MiniGame miniGame in miniGames)
        {
            if (miniGame.type == stationType)
            {
                miniGameGO = Instantiate(miniGame.minigamePrefab, miniGamePos.position, Quaternion.identity, miniGamePos);
            }
        }
    }

    public void Win()
    {
        RecipeManager.instance.ForwardStep();
        End();
    }

    public void Fail()
    {
        fails++;
        if (fails >= 3)
        {
            Lose();
        }
    }

    public void Lose()
    {
        End();
    }

    private void End()
    {
        UIManager.Instance.CloseMinigame();
        Destroy(miniGameGO);
    }
}
[Serializable]
public class MiniGame
{
    public StationType type;
    public GameObject minigamePrefab;
}
