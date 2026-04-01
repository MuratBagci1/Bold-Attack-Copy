using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{

    [SerializeField] private List<UpgradeSO> selectableUpgrades;
    [SerializeField] private List<UpgradeSingleUI> upgradeSlots;

    private List<UpgradeSO> upgradePool;

    private void Start()
    {
        ActionManager.OnUpgradeReachedFour += SetUpgradePool;
        ActionManager.OnXPTresholdReached += SetUpgradeSingleUI;
    }

    private void OnEnable()
    {
        SetUpgradePool(true);
        SetUpgradePool();
    }

    private void SetUpgradeSingleUI()
    {
        HashSet<int> usedIndexes = new HashSet<int>();

        foreach (UpgradeSingleUI upgrade in upgradeSlots)
        {
            int index;

            do
            {
                index = Random.Range(0, upgradePool.Count);
            } while (usedIndexes.Contains(index));

            usedIndexes.Add(index);
            upgrade.SetSingleUpgrade(upgradePool[index]);
        }
    }

    private void SetUpgradePool()
    {
        upgradePool = selectableUpgrades.Where(u => u.upgradeLevel < 4).ToList();
    }

    private void SetUpgradePool(bool isFirstUpgrade)
    {
        upgradePool = selectableUpgrades.Where(u => u.upgradeLevel < 4 && u.upgradeType == UpgradeType.weapon).ToList();
        SetUpgradeSingleUI();
    }
}