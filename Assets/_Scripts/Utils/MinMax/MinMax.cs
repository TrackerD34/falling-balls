using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinMax<T>
{
    public T Min => _min;
    public T Max => _max;

    [SerializeField] private T _min;
    [SerializeField] private T _max;

    public MinMax(T min, T max)
    {
        _min = min;
        _max = max;
    }
}

public static class MinMaxExtensions
{
    public static float Lerp(this MinMax<int> source, float value) => Mathf.Lerp(source.Min, source.Max, value);
    public static float Lerp(this MinMax<float> source, float value) => Mathf.Lerp(source.Min, source.Max, value);

    public static float InverseLerp(this MinMax<int> source, float value) => Mathf.InverseLerp(source.Min, source.Max, value);
    public static float InverseLerp(this MinMax<float> source, float value) => Mathf.InverseLerp(source.Min, source.Max, value);

    public static int GetRandomBetween(this MinMax<int> source) => Random.Range(source.Min, source.Max);
    public static float GetRandomBetween(this MinMax<float> source) => Random.Range(source.Min, source.Max);

    public static int Clamp(this MinMax<int> source, int value) => Mathf.Clamp(value, source.Min, source.Max);
    public static float Clamp(this MinMax<float> source, float value) => Mathf.Clamp(value, source.Min, source.Max);

}