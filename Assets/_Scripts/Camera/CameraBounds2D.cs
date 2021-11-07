using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBounds2D : MonoBehaviour
{
    private Camera _camera;

    public Bounds Bounds { get; private set; }

    private void Start()
    {
        _camera = GetComponent<Camera>();
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        var center = _camera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        var bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
        var size = (center - bottomLeft) * 2;
        Bounds = new Bounds(center, size);
    }
}
