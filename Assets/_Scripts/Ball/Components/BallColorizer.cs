using UnityEngine;
using System.Collections;

public class BallColorizer : MonoBehaviour, IBallComponent
{
    [SerializeField] private MeshRenderer _renderer;

    public void Initialize(Ball ball)
    {
        _renderer.material.color = ball.Settings.Color;
    }
}
