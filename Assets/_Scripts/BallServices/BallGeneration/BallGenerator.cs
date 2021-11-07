using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BallGenerator : MonoBehaviour
{
    public event Action<Ball> BallGenerated; 

    [SerializeField] private Ball _prefab;
    [SerializeField] private BallGenerationConfig _config;
    [SerializeField] private BallServices _services;

    private IEnumerator Start()
    {
        while (true)
        {
            CreateBall();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(_prefab, transform);

        ball.Construct(_config.CreateBallSettings());
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
