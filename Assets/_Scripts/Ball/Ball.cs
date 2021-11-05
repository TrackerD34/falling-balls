using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private AbstractMover _mover;

    private BallSettings _settings;

    private void Start()
    {
        
    }

    public void Construct(BallSettings settings)
    {
        _settings = settings;
    }

    public void InitializeComponents()
    {
        var components 
        if (_settings == null)
            throw new System.NullReferenceException();
    }

}
