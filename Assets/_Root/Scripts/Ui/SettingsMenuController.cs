using Ui;
using Game;
using Profile;
using UnityEngine;
using UnityEditor.SceneManagement;
using Tool;

internal class SettingsMenuController : BaseController
{
    private readonly ResourcePath _resoursePath = new ResourcePath("Prefabs/Ui/SettingsMenu");
    private readonly ProfilePlayer _profilePlayer;
    private readonly Transform _placeForUi;
    private readonly SettingsMenuView _view;

    public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;

        _view = LoadView(placeForUi);
        _view.Init(BackToMenu);
    }

    private SettingsMenuView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resoursePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<SettingsMenuView>();
    }

    private void BackToMenu() =>
        _profilePlayer.CurrentState.Value = GameState.Start;
}