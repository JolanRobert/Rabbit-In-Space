using UnityEngine;

[CreateAssetMenu(fileName = "New ParcelUpgrade", menuName = "ScriptableObjects/ParcelUpgrade")]
public class ParcelUpgradeSO : ScriptableObject {

    public UpgradeType upgradeType;

    public Sprite upgradeSprite;
    public string description;
    public int unlockCost;
    public bool isActivable;
}
