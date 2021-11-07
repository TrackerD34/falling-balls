using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallServices : MonoBehaviour
{
    [SerializeField] private BallGenerator _generator;
    [SerializeField] private CameraBounds2D _camerabounds;

    public BallGenerator Generator => _generator;
    public CameraBounds2D CameraBounds => _camerabounds;
}
