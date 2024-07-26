using System;
using UniRx;
using UnityEngine;

public class ScoreModel
{
    public ReactiveProperty<float> Score { get; private set; }

    public ScoreModel()
    {
        Score = new ReactiveProperty<float>(0);
    }
}
