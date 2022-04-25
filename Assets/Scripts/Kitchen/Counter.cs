using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour {
    
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject addButton, subButton;
    [SerializeField] private int startValue;
    [SerializeField] private Vector2Int minMaxValue;
    
    public int currentValue;

    void Start() {
        Reset();
    }
    
    public void Add() {
        currentValue++;
        CheckValues();
    }

    public void Substract() {
        currentValue--;
        CheckValues();
    }

    public void Reset() {
        currentValue = Mathf.Clamp(startValue, minMaxValue.x, minMaxValue.y);
        CheckValues();
    }

    private void CheckValues() {
        subButton.SetActive(currentValue != minMaxValue.x);
        addButton.SetActive(currentValue != minMaxValue.y);
        text.text = currentValue.ToString();
        RecipeAmountPrompt.Instance.UpdateIngredientSlots();
    }
}
