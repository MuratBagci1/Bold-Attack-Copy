using System.Collections;
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
        SetUpgradePool();

        SetUpgradeSingleUI();
    }

    private void SetUpgradeSingleUI()
    {
        foreach (UpgradeSingleUI upgrade in upgradeSlots)
        {
            int upgradeIndex = Random.Range(0, upgradePool.Count);

            upgrade.SetSingleUpgrade(upgradePool[upgradeIndex]);
        }
    }

    private void SetUpgradePool()
    {
        upgradePool = selectableUpgrades.Where(u => u.upgradeLevel < 4).ToList();
    }
}