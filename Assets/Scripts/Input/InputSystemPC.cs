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
    public Vector2ReactiveProperty MoveInput { get; } = new();
    public Vector3ReactiveProperty MouseClick { get; } = new();

    public InputSystemPC(NewInputSystem input)
    {
        _input = input;
    }
    
    private void Movable()
    {
        MoveInput.Value = GetMovement();
    }

    private Vector2 GetMovement()
    {
        return _input.Move.MoveWithWASD.ReadValue<Vector2>();
    }

    public void Initialize()
    {
        _input.Enable();
        _input.Mouse.Fire.performed += OnFire;
        
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                Movable();
            })
            .AddTo(_compositeDisposable);
        
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        MouseClick.Value = Input.mousePosition;
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
