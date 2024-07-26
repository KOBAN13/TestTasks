using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ButtonsView : MonoBehaviour
{
    [SerializeField] private ButtonConfig[] _buttons;

    private CompositeDisposable _compositeDisposable = new();
    
    [Inject]
    public void Construct(ButtonModel model)
    {
        foreach (var buttonConfig in _buttons)
        {
            SubscribeButton(buttonConfig, model);
        }
    }

    private void SubscribeButton(ButtonConfig buttonConfig, ButtonModel model)
    {
        buttonConfig.Button.OnClickAsObservable()
            .Subscribe(_ => model.PressButton(buttonConfig.ButtonId))
            .AddTo(_compositeDisposable);
    }
}
