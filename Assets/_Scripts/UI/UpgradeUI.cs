using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{

    [SerializeField] private List<UpgradeSO> selectableUpgrades;
    [SerializeField] private List<UpgradeSingleUI> upgradeSlots;

    private List<UpgradeSO> upgradePool;

    private void Awake()
    {
        upgradePool = new List<UpgradeSO>();
        SetInitialPool(selectableUpgrades);
    }

    private void OnEnable()
    {
        ActionManager.OnXPTresholdReached += ShowAllUpgrades;
        ActionManager.OnNewWave += ShowWeaponUpgrades;
    }

    private void OnDisable()
    {
        ActionManager.OnXPTresholdReached -= ShowAllUpgrades;
        ActionManager.OnNewWave -= ShowWeaponUpgrades;
    }

    private void SetUpgradeSingleUI(List<UpgradeSO> pool)
    {
        int usableSlots = Mathf.Min(upgradeSlots.Count, pool.Count);

        //work on this, game doesn't continue
        if (usableSlots <= 0)
        {
            ActionManager.OnGamePausedCancelled?.Invoke();
            return;
        }

        HashSet<int> usedIndexes = new HashSet<int>();
        for (int i = 0; i < usableSlots; i++)
        {
            int index;

            do
            {
                index = Random.Range(0, pool.Count);
            } while (usedIndexes.Contains(index));

            usedIndexes.Add(index);
            upgradeSlots[i].SetSingleUpgrade(pool[index]);
        }
    }

    private void ShowAllUpgrades()
    {
        var pool = GetFilteredPool(false);

        SetUpgradeSingleUI(pool);
    }
    private void ShowWeaponUpgrades()
    {
        var pool = GetFilteredPool(true);

        SetUpgradeSingleUI(pool);
    }

    private List<UpgradeSO> GetFilteredPool(bool onlyWeapons)
    {
        var pool = upgradePool.Where(x => x.upgradeLevel < 5);

        if (onlyWeapons)
            pool = pool.Where(x => x.upgradeType == UpgradeType.weapon);

        return pool.ToList();
    }

    private void SetInitialPool(List<UpgradeSO> newList)
    {
        foreach (UpgradeSO upgrade in selectableUpgrades)
        {
            UpgradeSO newUpgrade = Instantiate<UpgradeSO>(upgrade);
            upgradePool.Add(newUpgrade);
        }
    }
}