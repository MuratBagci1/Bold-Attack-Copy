using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/PistolSO", order = 5)]
public class WeaponSO : UpgradeSO
{
    public GameObject weaponPrefab;
    public float increase;
    public float cooldown;
    public float damage;

    public override void Apply(GameObject player)
    {
        if (!isInitiated)
        {
            GameObject weaponTemp = Instantiate(weaponPrefab, player.gameObject.transform);
            ActionManager.OnWeaponAdded?.Invoke(weaponTemp);

            weaponTemp.GetComponent<Weapon>().Innit(damage, cooldown);

            isInitiated = true;
        }
        else
        {
            damage += increase;
        }
    }
}