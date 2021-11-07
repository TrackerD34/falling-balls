using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ball : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    public BallSettings Settings { get; private set; }
    public Vector3 Size => _collider.bounds.size;

    public void Construct(BallSettings settings)
    {
        Settings = settings;
        InitializeComponents();
    }

    public void InitializeComponents()
    {
        var components = GetComponentsInChildren<IBallComponent>().ToList();
        components.ForEach(item => item.Initialize(this));
    }
}
