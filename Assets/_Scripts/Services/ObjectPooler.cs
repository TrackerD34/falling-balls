using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    private List<PooledObject<Component>> _pooledItems = new List<PooledObject<Component>>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    public PooledObject<Component> CreatePool<T>(T prefab, int count) where T : Component
    {
        return InitItemPool(prefab, count);
    }

    public T GetItemFromPoolAtPosition<T>(T prefab, Vector3 position) where T : Component
    {
#if UNITY_EDITOR
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS NULL");
            return null;
        }
#endif
        if (_pooledItems.FirstOrDefault(item => item.Prefab == prefab) is var pool && pool == null)
            pool = CreatePool(prefab, 1);

        return pool.GetPooledAtPosition(position) as T;
    }

    private PooledObject<Component> InitItemPool<T>(T prefab, int count = 1) where T : Component
    {
        if (_pooledItems.FirstOrDefault(item => item.Prefab == prefab) is var pool && pool != null)
        {
            pool.ExpandPool(count);
            return pool;
        }
        var newPool = new PooledObject<Component>(prefab, count);
        _pooledItems.Add(newPool);

        return newPool;
    }
}

public static class ObjectPoolerExtensions
{
    public static T GetPooledAtPosition<T>(this T prefab, Vector3 position) where T : Component
    {
        return ObjectPooler.Instance.GetItemFromPoolAtPosition(prefab, position);
    }
}