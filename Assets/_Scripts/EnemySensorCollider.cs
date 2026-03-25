using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemySensorCollider : MonoBehaviour
{
    [SerializeField, ReadOnly] public static List<Transform> detectedEnemyList;

    private void Awake()
    {
        detectedEnemyList = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable component))
        {
            detectedEnemyList.Add(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable component))
        {
            detectedEnemyList.Remove(collision.transform);

            Debug.Log("Enemy Exited Collision");
        }
    }
}