using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;
    [Header("Attributes")]
    public int itemNumber;

    public void SetupSlot(EnumManager.ItemType newType, Sprite sprite, int newAmount)
    {
        itemNumber = newAmount;
        image.sprite = sprite;
        UpdateSlot();
    }
    
    public void UpdateAmount(int amount)
    {
        itemNumber = amount;
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        amountText.text = itemNumber.ToString();
    }
}
