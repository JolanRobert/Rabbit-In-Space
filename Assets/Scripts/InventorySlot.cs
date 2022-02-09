using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;
    private FridgeInventoryManager inventoryManager;
    [Header("Attributes")]
    [SerializeField]private List<Sprite> sprites;
    public FridgeInventoryManager.ItemTypes itemType;
    public int itemNumber;
    private void Start()
    {
        inventoryManager = GetComponentInParent<FridgeInventoryManager>();
    }

    public void SetupSlot(FridgeInventoryManager.ItemTypes newType, int newAmount)
    {
        itemType = newType;
        itemNumber = newAmount;
        switch (itemType)
        {
            case FridgeInventoryManager.ItemTypes.MOONRICE:
                image.sprite = sprites[0];
                break;
            case FridgeInventoryManager.ItemTypes.STARBERRY:
                image.sprite = sprites[1];
                break;
            case FridgeInventoryManager.ItemTypes.NEBULAZUKI:
                image.sprite = sprites[2];
                break;
            default:
                image.sprite = null;
                amountText.text = "???";
                break;
        }
        UpdateSlot();
    }
    
    public void AddItems(int amount)
    {
        itemNumber += amount;
        UpdateSlot();
    }
    void UpdateSlot()
    {
        amountText.text = itemNumber.ToString();
    }
}
