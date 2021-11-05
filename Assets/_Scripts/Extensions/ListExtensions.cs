using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class ListExtensions
{
    private static System.Random rnd = new System.Random();

    public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
    {
        return list.OrderBy(arg => System.Guid.NewGuid()).Take(elementsCount).ToList();
    }
    //TODO: mb return list[randomNumber]
    public static T GetRandomElement<T>(this IEnumerable<T> list)
    {
        return list.ElementAtOrDefault(Random.Range(0, list.Count()));
        //return list.Shuffle().FirstOrDefault();
    }

    public static List<T> Shuffle<T>(this IEnumerable<T> list)
    {
        return list.OrderBy(arg => System.Guid.NewGuid()).ToList();
    }

    //public static T GetClosest<T>(this ICollection<T> collection, Vector3 toPosition) where T : MonoBehaviour
    //{
    //    return collection.OrderBy(item => (item.transform.position - toPosition).sqrMagnitude).FirstOrDefault();
    //}
    ////TODO: remove item != null
    //public static T GetClosest<T>(this ICollection<T> collection, Vector3 toPosition, float maxDistance) where T : MonoBehaviour
    //{
    //    var collectionCopy = collection.ToList();

    //    collectionCopy.RemoveAll(item => item != null && (item.transform.position - toPosition).sqrMagnitude > maxDistance * maxDistance);
    //    return collectionCopy.GetClosest(toPosition);
    //}

    public static T GetClosest<T>(this IEnumerable<T> list, Vector3 toPosition) where T : Component
    {
        return list.OrderBy(item => (item.transform.position - toPosition).sqrMagnitude).FirstOrDefault();
    }

    public static Vector3 GetClosest(this IEnumerable<Vector3> list, Vector3 toPosition)
    {
        return list.OrderBy(item => (item - toPosition).sqrMagnitude).FirstOrDefault();
    }

    public static IEnumerable<T> GetClosest<T>(this IEnumerable<T> list, Vector3 toPosition, int count) where T : MonoBehaviour
    {
        return list.OrderBy(item => (item.transform.position - toPosition).sqrMagnitude).Take(count);
    }

    public static List<T> GetAllInRange<T>(this List<T> list, Vector3 toPosition, float maxDistance = Mathf.Infinity) where T : MonoBehaviour
    {
        return list.FindAll(item => item.gameObject.activeInHierarchy && (item.transform.position - toPosition).sqrMagnitude < maxDistance * maxDistance).ToList();
    }

    public static IEnumerable<T> OrderByDistance<T>(this IEnumerable<T> list, Vector3 from) where T : MonoBehaviour
    {
        return list.OrderBy(item => Vector3.Distance(item.transform.position, from));
    }
}