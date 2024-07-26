using SaveSystem;
using Zenject;

public class RecordModel : IInitializable
{
    private JsonDataContext _jsonDataContext;
    private RecordView _recordView;

    public RecordModel(JsonDataContext jsonDataContext, RecordView recordView)
    {
        _jsonDataContext = jsonDataContext;
        _recordView = recordView;
    }

    public async void Initialize()
    {
        await _jsonDataContext.Load();
        
        _recordView?.ShowRecord(_jsonDataContext.Score);
    }
}
