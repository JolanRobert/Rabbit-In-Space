using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObjects/Customer")]
public class CustomerSO : ScriptableObject {
    
    [Header("Global Infos")]
    public CustomerType customerType;
    public int impatienceLimit;

    [Header("Sprites")]
    public Sprite customerSprite;
}