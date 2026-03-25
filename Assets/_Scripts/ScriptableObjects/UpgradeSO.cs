using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeScriptableObject", order = 4)]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public int upgradeLevel;
    public Sprite upgradeSprite;

    public void UpgradeTier()
    {
        upgradeLevel += 1;
    }
}