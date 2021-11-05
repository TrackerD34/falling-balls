using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMover : MonoBehaviour
{
    [SerializeField] protected Transform _objectToMove;
    [SerializeField] private float _speed;

    private Coroutine _moveRoutine;

    public void Construct(float speed)
    {
        _speed = speed;
    }

    public void StartMovement()
    {
        Stop();
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    public void Stop()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            var direction = GetMoveDirection();
            var velocity = direction.normalized * _speed;
            _objectToMove.Translate(velocity * Time.deltaTime);
            yield return null;
        }
    }

    protected abstract Vector3 GetMoveDirection();
}
