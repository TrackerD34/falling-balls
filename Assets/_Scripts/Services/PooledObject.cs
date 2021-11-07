using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PooledObject<T> where T : Component
{
    public readonly T Prefab;
    private List<T> _list = new List<T>();

    public PooledObject(T prefab)
    {
        Prefab = prefab;
    }

    public PooledObject(T prefab, int count)
    {
        Prefab = prefab;
        ExpandPool(count);
    }

    public void ExpandPool(int count)
    {
        while (_list.Count < count)
            CreatePooledItem();
    }

    public T GetPooledAtPosition(Vector3 position)
    {
        var result = GetPooledItem();
        result.transform.position = position;
        result.gameObject.SetActive(true);
        return result;
    }

    private T GetPooledItem()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            var item = _list[i];
            if (item != null && !item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }
        return CreatePooledItem();
    }

    public void Reset()
    {
        _list.ForEach(item =>
        {
            if (item != null)
                item.gameObject.SetActive(false);
        });
        _list.Clear();
    }

    public void Copy<TSource>(PooledObject<TSource> other) where TSource : Component
    {
        _list = other._list.Select(item => item as T).ToList();
    }

    private T CreatePooledItem()
    {
        var newItem = GameObject.Instantiate(Prefab);
        _list.Add(newItem);
        newItem.gameObject.SetActive(false);
        return newItem;
    }
}
