using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, IPoolable
{
    private T prefab;
    private Transform parent;
    private Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.name = prefab.name;
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab, parent);
        obj.name = prefab.name;
        obj.gameObject.SetActive(true);
        obj.OnSpawn();
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.OnDespawn();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}