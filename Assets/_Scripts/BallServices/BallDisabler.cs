using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BallDisabler : MonoBehaviour
{
    [SerializeField] private BallServices _services;
    [SerializeField] private DisableCondition _disableCondition;

    private List<Ball> _visibleBalls = new List<Ball>();
    private CanBeDisabled _canBeDisabled;

    private void OnEnable()
    {
        _services.Generator.BallGenerated += OnBallGenerated;
    }

    private void OnDisable()
    {
        _services.Generator.BallGenerated -= OnBallGenerated;
    }

    private void Start()
    {
        UpdateDisableCheck();
    }

    private void Update()
    {
        _visibleBalls.ForEach(TryDisable);
        _visibleBalls.RemoveAll(item => !item.gameObject.activeInHierarchy);
    }

    private void OnBallGenerated(Ball ball)
    {
        _visibleBalls.Add(ball);
    }

    private void UpdateDisableCheck()
    {
        switch (_disableCondition)
        {
            case DisableCondition.When_Touches_Bottom_Line:
                _canBeDisabled = TouchesBottomLine;
                break;
            case DisableCondition.When_Under_Bottom_Line:
                _canBeDisabled = IsUnderBottomLine;
                break;
        }
    }

    private void TryDisable(Ball ball)
    {
        if (_canBeDisabled(ball))
        {
            ball.gameObject.SetActive(false);
        }
    }

    private bool TouchesBottomLine(Ball ball)
    {
        return ball.transform.position.y - ball.Size.y / 2.0f < _services.CameraBounds.Bounds.min.y;
    }

    private bool IsUnderBottomLine(Ball ball)
    {
        return ball.transform.position.y + ball.Size.y / 2.0f < _services.CameraBounds.Bounds.min.y;
    }

    private enum DisableCondition
    {
        When_Touches_Bottom_Line,
        When_Under_Bottom_Line
    }

    private delegate bool CanBeDisabled(Ball ball);

#if UNITY_EDITOR
    [CustomEditor(typeof(BallDisabler))]
    private class BallDisablerEditor : Editor
    {
        private BallDisabler _controller;

        private void OnEnable()
        {
            _controller = target as BallDisabler;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if(Application.isPlaying && EditorGUI.EndChangeCheck())
            {
                _controller.UpdateDisableCheck();
            }
        }
    }
#endif
}
