using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private BallSettings _settings;

    public void Construct(BallSettings settings)
    {
        _settings = settings;
    }
}
