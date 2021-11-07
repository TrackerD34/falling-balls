using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScaler : MonoBehaviour, IBallComponent
{
    [SerializeField] private Transform _objectToScale;

    public void Initialize(Ball ball)
    {
        _objectToScale.localScale = Vector3.one * ball.Settings.Scale;
    }
}
