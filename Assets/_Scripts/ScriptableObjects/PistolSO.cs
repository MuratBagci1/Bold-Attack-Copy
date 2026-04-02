using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/PistolSO", order = 5)]
public class PistolSO : UpgradeSO
{
    public GameObject weaponPrefab;
    public float increase;

    private GameObject weaponTemp;
    private Weapon weaponData;
    private bool isInitiated;

    public override void Apply(GameObject player)
    {
        if (!isInitiated)
        {
            weaponTemp = Instantiate(weaponPrefab, player.gameObject.transform);
            ActionManager.OnWeaponAdded?.Invoke(weaponTemp);
            weaponData = weaponTemp.GetComponent<Weapon>();

            isInitiated = true;
        }
        else if (weaponData != null)
        {
            weaponData.weaponDamage += increase;
        }
    }
}