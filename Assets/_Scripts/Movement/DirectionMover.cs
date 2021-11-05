using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMover : AbstractMover, IBallComponent
{
    [SerializeField] private Vector3 _direction;

    public void Initialize(BallSettings settings)
    {
        Construct(settings.Speed);
    }

    protected override Vector3 GetMoveDirection()
    {
        return _direction;
    }
}
