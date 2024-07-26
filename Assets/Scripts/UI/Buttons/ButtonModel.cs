using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ButtonModel
{
    private readonly Dictionary<string, Subject<Unit>> _buttonStreams = new();
    
    public IObservable<Unit> GetButtonStream(string buttonId)
    {
        if (_buttonStreams.ContainsKey(buttonId) == false)
        {
            _buttonStreams[buttonId] = new Subject<Unit>();
        }
        return _buttonStreams[buttonId];
    }
    
    public void PressButton(string buttonId)
    {
        if (_buttonStreams.ContainsKey(buttonId) == true)
        {
            _buttonStreams[buttonId].OnNext(Unit.Default);
        }
    }
}
