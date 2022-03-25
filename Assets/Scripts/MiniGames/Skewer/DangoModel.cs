using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Skewer {
public class DangoModel : MonoBehaviour {
    public static DangoModel Instance;

    [SerializeField] private int nbSkewer;
    [SerializeField] private int dangoPerSkewer;
    [SerializeField] private Vector2 minMaxPerColor;

    [SerializeField] private List<Image> dangoImages;
    [SerializeField] private List<DangoItem> dangoItems;
    
    private DangoColor[,] model;
    public DangoColor[,] player;

    void Awake() {
        Instance = this;
    }

    void Start() {
        GenerateModel();
    }

    private void GenerateModel() {
        model = new DangoColor[nbSkewer, dangoPerSkewer];
        player = new DangoColor[nbSkewer, dangoPerSkewer];
        
        int index = 0;
        for (int i = 0; i < nbSkewer; i++) {
            for (int j = 0; j < dangoPerSkewer; j++) {
                int random = Random.Range(1, Enum.GetValues(typeof(DangoColor)).Length);
                DangoColor rdmColor = (DangoColor)Enum.GetValues(typeof(DangoColor)).GetValue(random);

                model[i, j] = rdmColor;
                dangoImages[index].sprite = dangoItems.FirstOrDefault(d => d.dangoColor == rdmColor)?.dangoSprite;

                player[i, j] = DangoColor.NONE;

                index++;
            }
        }
    }

    public void SetPlayerDango(int line, int column, DangoColor dangoColor) {
        player[line,column] = dangoColor;
        CompareModelPlayer();
    }

    private void CompareModelPlayer() {
        int correctDangos = 0;
        
        for (int i = 0; i < nbSkewer; i++) {
            for (int j = 0; j < dangoPerSkewer; j++) {
                if (model[i, j].Equals(player[i, j])) correctDangos++;
            }
        }

        if (correctDangos == nbSkewer * dangoPerSkewer) {
            MinigameManager.instance.EndMinigame(true);
        }
    }
}

[Serializable]
public class DangoItem {
    public DangoColor dangoColor;
    public Sprite dangoSprite;
}

public enum DangoColor {
    NONE,
    YELLOW,
    PINK,
    GREEN,
    ORANGE,
    PURPLE
}
}