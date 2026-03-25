using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 2)]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float cooldown;
    public float damage;
}