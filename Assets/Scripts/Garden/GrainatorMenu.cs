using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GrainatorMenu : MonoBehaviour {
    
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private float radius;
    [SerializeField] private Image selectedFoodSprite;
    
    private List<GameObject> entries = new List<GameObject>();
    
    void Start() {
        GetComponent<Button>().onClick.AddListener(Toggle);
        AddEntry(null,FoodType.NONE);

        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            AddEntry(foodSo.foodSprite,foodSo.foodType);
        }
    }

    private void AddEntry(Sprite foodSprite, FoodType foodType) {
        GameObject entry = Instantiate(entryPrefab, transform);
        entry.transform.GetChild(0).GetComponent<Image>().sprite = foodSprite;
        entry.GetComponent<Button>().onClick.AddListener(() => {
            Toggle();
            OnSelectFood(foodType);
        });
        entries.Add(entry);
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

    private bool isToggle;
    public void Toggle() {
        if (isToggle) Close();
        else Open();
        isToggle = !isToggle;
    }

    private void OnSelectFood(FoodType foodType) {
        ParcelMenuEntry pme = transform.parent.GetComponent<ParcelMenuEntry>();
        GardenManager.Instance.myParcel.foodList[pme.foodSlot].SetGrainatorFood(foodType);
        SetFoodSprite(foodType);
    }

    public void SetFoodSprite(FoodType foodType) {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            if (foodType != foodSo.foodType) continue;
            selectedFoodSprite.sprite = foodSo.foodSprite;
            return;
        }

        selectedFoodSprite.sprite = null;
    }
}
