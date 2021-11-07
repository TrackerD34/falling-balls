using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balls/Configs/Ball Generation Config")]
public class BallGenerationConfig : ScriptableObject
{
    [SerializeField] private MinMax<float> _scale;
    [SerializeField] private MinMax<float> _speed;
    [SerializeField] private MinMax<int> _points;
    [SerializeField] private MinMax<int> _damage;
    [SerializeField] private List<Color> _colors;

    public BallSettings CreateBallSettings()
    {
        return new BallSettings()
        {
            Scale = _scale.GetRandomBetween(),
            Speed = _speed.GetRandomBetween(),
            Points = _points.GetRandomBetween(),
            Damage = _damage.GetRandomBetween(),
            Color = _colors.GetRandomElement()
        };
    }
}
