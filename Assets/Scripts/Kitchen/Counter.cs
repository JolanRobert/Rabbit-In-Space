using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    private UnityEvent valueChanged;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject addButton, subButton;
    [SerializeField] public int value;
    [SerializeField] private int min, max;

    void Start()
    {
        value = Mathf.Clamp(value, min, max);
        CheckValues();
    }
    
    public void Add()
    {
        value++;
        CheckValues();
    }

    public void Substract()
    {
        value--;
        CheckValues();
    }

    private void CheckValues()
    {
        if (value == min) subButton.SetActive(false);
        else subButton.SetActive(true);
        
        if (value == max) addButton.SetActive(false);
        else addButton.SetActive(true);

        text.text = value.ToString();
        RecipeAmountPrompt.instance.UpdateIngredientSlots();
    }
}
