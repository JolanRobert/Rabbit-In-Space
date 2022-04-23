using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObjects/Customer")]
public class CustomerSO : ScriptableObject {
    
    [Header("Global Infos")]
    public CustomerType customerType;
    public int impatienceLimit;
    public int xpReward;

    [Header("Directory Infos")]
    public int nbBeforeReveal;
    public string description;

    [Header("Sprites")]
    public Sprite customerSprite;
}
