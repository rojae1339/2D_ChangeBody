public class PartsUIPresenter
{
    private PlayerUIView _view;
    private Player _modelPlayer;

    public PartsUIPresenter(PlayerUIView view, Player model)
    {
        _view = view;
        _modelPlayer = model;

        // 매니저 초기화 완료 후 UI 생성
        Managers.Managers.OnManagerLoadInitialized += OnManagersReady;
    }

    private void OnManagersReady()
    {
        _view.CreateDropPartsUI();
        Managers.Managers.OnManagerLoadInitialized -= OnManagersReady;
    }
}