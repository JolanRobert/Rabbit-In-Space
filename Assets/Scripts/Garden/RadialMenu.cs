using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour {

    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private float radius;
    private List<GameObject> entries = new List<GameObject>();

    private bool isToggle;
    
    void Start() {
        foreach (FoodSO foodSo in GardenManager.Instance.foodList) {
            GameObject entry = Instantiate(entryPrefab, transform);
            entry.GetComponent<RadialMenuEntry>().Init(foodSo);
            entry.GetComponent<Button>().onClick.AddListener(Toggle);
            entries.Add(entry);
        }
    }

    private void Open() {
        float radiansOfSeparation = Mathf.PI * 2 / entries.Count;
        for (int i = 0; i < entries.Count; i++) {
            float x = Mathf.Sin(radiansOfSeparation * i) * radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * radius;

            RectTransform rect = entries[i].GetComponent<RectTransform>();
            rect.DOComplete();
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(.1f * i);
            rect.DOAnchorPos(new Vector2(x,y), 0.3f).SetEase(Ease.OutQuad).SetDelay(.1f * i);
        }
    }

    private void Close() {
        for (int i = 0; i < entries.Count; i++) {
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            rect.DOComplete();
            rect.DOScale(Vector3.zero, 0.3f);
            rect.DOAnchorPos(Vector2.zero, 0.3f);
        }
    }

    public void Toggle() {
        if (isToggle) Close();
        else Open();
        isToggle = !isToggle;
    }
}
