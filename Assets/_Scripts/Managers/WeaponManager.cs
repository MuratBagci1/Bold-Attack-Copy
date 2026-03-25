using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float weaponPlaceRadius;

    private Player playerInstance;

    private void Start()
    {
        playerInstance = Player.instance;

        ActionManager.OnWeaponAdded += SetWeaponPlaces;
    }

    private void SetWeaponPlaces(List<GameObject> weapons, Transform player)
    {
        int weaponCount = weapons.Count;

        for (int i = 0; i < weaponCount; i++)
        {
            float angle = i * (360f/weaponCount) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 targetPos = player.position + (Vector3)(dir * weaponPlaceRadius);

            weapons[i].transform.position = targetPos;
            weapons[i].transform.up = dir;
            weapons[i].transform.parent = player;
        }
    }
}