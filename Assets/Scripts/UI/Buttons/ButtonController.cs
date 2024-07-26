using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ButtonController : MonoBehaviour
{
    private CompositeDisposable _compositeDisposable = new();
    
    [Inject]
    public void Construct(ButtonModel model)
    {
        model.GetButtonStream("GoToMenu")
            .Subscribe(_ => LoadScene("Menu"))
            .AddTo(_compositeDisposable);
        
        model.GetButtonStream("Start")
            .Subscribe(_ => LoadScene("Game"))
            .AddTo(_compositeDisposable);
        
        model.GetButtonStream("Restart")
            .Subscribe(_ => RestartScene())
            .AddTo(_compositeDisposable);
    }
    
    private void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    private void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
}
