using System;
using InputSystem;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemPC : IInputSystem, IInitializable, IDisposable
{
    private NewInputSystem _input;
    private CompositeDisposable _compositeDisposable = new();
    public Vector2 Input { get; private set; }
    public Vector3ReactiveProperty MouseClick { get; } = new();

    public InputSystemPC(NewInputSystem input)
    {
        _input = input;
    }

    private void GetMovement()
    {
        Input = _input.Move.MoveWithWASD.ReadValue<Vector2>();
    }

    public void Initialize()
    {
        _input.Enable();
        _input.Mouse.Fire.performed += OnFire;
        
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                GetMovement();
            })
            .AddTo(_compositeDisposable);
        
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        MouseClick.Value = UnityEngine.Input.mousePosition;
    }

    public void Dispose()
    {
        _input.Mouse.Fire.performed -= OnFire;
        _input.Disable();
        _input.Dispose();
        _compositeDisposable.Clear();
        _compositeDisposable.Dispose();
    }
}
