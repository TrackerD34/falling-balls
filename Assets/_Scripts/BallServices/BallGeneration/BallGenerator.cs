using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class BallGenerator : MonoBehaviour
{
    private const int PoolSize = 10;

    public event Action<Ball> BallGenerated;

    [SerializeField] private Ball _prefab;
    [SerializeField] private BallGenerationConfig _config;
    [SerializeField] private BallServices _services;

    private IEnumerator Start()
    {
        ObjectPooler.Instance.CreatePool(_prefab, PoolSize);
        while (true)
        {
            yield return CreateBall();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator CreateBall()
    {
        var ball = _prefab.GetPooledAtPosition(_services.CameraBounds.Bounds.max + Vector3.one * 10);
        ball.Construct(_config.CreateBallSettings());
        yield return new WaitForEndOfFrame();
        SetOnStartPosition(ball);
        BallGenerated?.Invoke(ball);
    }

    private void SetOnStartPosition(Ball ball)
    {
        var ballSize = ball.Size;
        var bounds = _services.CameraBounds.Bounds;

        var spawnPosition = bounds.max;
        spawnPosition.x = Random.Range(bounds.min.x + ballSize.x / 2.0f, bounds.max.x - ballSize.x / 2.0f);
        spawnPosition.y += ballSize.y / 2.0f;

        ball.transform.position = spawnPosition;
    }
}
