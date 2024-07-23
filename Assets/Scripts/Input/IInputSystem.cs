using System;
using UniRx;
using UnityEngine;

namespace InputSystem
{
    public interface IInputSystem
    {
        Vector2ReactiveProperty MoveInput { get; }
        Vector3ReactiveProperty MouseClick { get; }
    }
}