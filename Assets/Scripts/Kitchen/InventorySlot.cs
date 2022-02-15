using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;
    [Header("Attributes")]
    public ItemType type;

    private void OnEnable()
    {
        UpdateSlot();
    }

    public void SetupSlot(ItemType newType, Sprite sprite)
    {
        type = newType;
        image.sprite = sprite;
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        amountText.text = FoodDataManager.Instance.Load(type.ToString()).amount.ToString();
    }
}
