using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, object> pools = new Dictionary<string, object>();

    void Awake()
    {
        Instance = this;
    }

    public void CreatePool<T>(T prefab, int size) where T : MonoBehaviour, IPoolable
    {
        if (!pools.ContainsKey(prefab.gameObject.name))
        {
            GameObject parentGO = new GameObject(prefab.name + "_Pool");
            parentGO.transform.parent = this.transform;

            pools[prefab.gameObject.name] = new ObjectPool<T>(prefab, size, parentGO.transform);
        }
    }

    public T Get<T>(T prefab) where T : MonoBehaviour, IPoolable
    {
        return ((ObjectPool<T>)pools[prefab.gameObject.name]).Get();
    }

    public void Return<T>(T obj) where T : MonoBehaviour, IPoolable
    {
        ((ObjectPool<T>)pools[obj.gameObject.name]).ReturnToPool(obj);
    }
}
