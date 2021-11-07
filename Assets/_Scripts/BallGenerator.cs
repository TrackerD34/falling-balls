using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private Ball _prefab;
    [SerializeField] private BallGenerationConfig _config;
    [SerializeField] private CameraBounds2D _cameraBounds;

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
        CalculateStartPosition(ball);
    }

    private void CalculateStartPosition(Ball ball)
    {
        var ballSize = ball.Size;

        var bounds = _cameraBounds.Bounds;
        var spawnPosition = bounds.max;
        spawnPosition.x = Random.Range(bounds.min.x + ballSize.x / 2.0f, bounds.max.x - ballSize.x / 2.0f);
        spawnPosition.y += ballSize.y / 2.0f;
        ball.transform.position = spawnPosition;
    }
}
