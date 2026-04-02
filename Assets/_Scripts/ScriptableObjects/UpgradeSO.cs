using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeScriptableObject", order = 4)]
public abstract class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public int upgradeLevel;
    public Sprite upgradeSprite;
    public UpgradeType upgradeType;

    public void UpgradeTier()
    {
        upgradeLevel += 1;
    }

    public abstract void Apply(GameObject player);
}

public enum UpgradeType
{
    weapon,
    power
}