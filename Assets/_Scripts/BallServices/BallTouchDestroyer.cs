using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallTouchDestroyer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem _destroyFx;

    private void Start()
    {
        ObjectPooler.Instance.CreatePool(_destroyFx, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }
    }

    private void OnMouseButtonDown()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit))
        {
            if(hit.transform.GetComponentInParent<Ball>() is var ball && ball != null)
            {
                ball.gameObject.SetActive(false);
                StartCoroutine(DestroyBall(ball));
            }
        }
    }

    private IEnumerator DestroyBall(Ball ball)
    {
        ball.gameObject.SetActive(false);
        var fx = _destroyFx.GetPooledAtPosition(ball.transform.position);
        fx.transform.localScale = Vector3.one * ball.Settings.Scale;
        var systems = fx.GetComponentsInChildren<ParticleSystem>().ToList();
        systems.ForEach(item =>
        {
            var main = item.main;
            main.startColor = ball.Settings.Color;
        });
        yield return new WaitForSeconds(_destroyFx.main.duration);
        fx.gameObject.SetActive(false);
    }
}
