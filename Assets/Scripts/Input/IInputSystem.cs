using System;
using UniRx;
using UnityEngine;

namespace InputSystem
{
    public interface IInputSystem
    {
        Vector2 Input { get; }
        Vector3ReactiveProperty MouseClick { get; }
    }
}