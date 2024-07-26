using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private ScoreModel _scoreModel;
    private CompositeDisposable _compositeDisposable = new();
    
    [Inject]
    public void Construct(ScoreModel model)
    {
        _scoreModel = model;
        SubscribeOnScoreChange();
        UpdateScoreDisplay(_scoreModel.Score.Value);
    }

    private void SubscribeOnScoreChange()
    {
        _scoreModel.Score
            .Subscribe(OnScoreChanged)
            .AddTo(_compositeDisposable);
    }
    
    private void OnScoreChanged(float newScore)
    {
        UpdateScoreDisplay(newScore);
    }

    private void UpdateScoreDisplay(float score)
    {
        _scoreText.text = $"Счет: {score}";
    }
}
