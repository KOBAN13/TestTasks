using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IMenu, IShowRecord
{
    [SerializeField] private GameObject _gameUIMenu;
    [SerializeField] private GameObject _looseUIMenu;
    [SerializeField] private TextMeshProUGUI _newRecord;
    
    private event Action OnPlayerDied;
    private event Action OnNewRecordScore; 
    
    public void OnOnNewRecordScore()
    {
        OnNewRecordScore?.Invoke();
    }

    public void OnOnPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
    
    private void OnEnable()
    {
        OnPlayerDied += ShowLooseMenu;
        OnNewRecordScore += ShowNewRecord;
    }

    private void ShowNewRecord()
    {
        _newRecord.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        OnPlayerDied -= ShowLooseMenu;
        OnNewRecordScore -= ShowNewRecord;
    }

    private void ShowLooseMenu()
    {
        _gameUIMenu.SetActive(false);
        _looseUIMenu.SetActive(true);
        Time.timeScale = 0;
    }
}

public interface IShowRecord
{
    void OnOnNewRecordScore();
}

public interface IMenu
{
    void OnOnPlayerDied();
}
