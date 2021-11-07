using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
    {
        return list.OrderBy(arg => System.Guid.NewGuid()).Take(elementsCount).ToList();
    }

    public static T GetRandomElement<T>(this IEnumerable<T> list)
    {
        return list.ElementAtOrDefault(Random.Range(0, list.Count()));
    }

    public static List<T> Shuffle<T>(this IEnumerable<T> list)
    {
        return list.OrderBy(arg => System.Guid.NewGuid()).ToList();
    }
}