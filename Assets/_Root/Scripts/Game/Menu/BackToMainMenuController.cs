using Profile;
using Tool;
using UnityEngine;

namespace Game.Menu
{
    internal class BackToMainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/BackToMainMenuView");

        private readonly BackToMainMenuView _view;
        private readonly ProfilePlayer _profilePlayer;

        public BackToMainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(BackToMainMenu);
        }

        private BackToMainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BackToMainMenuView>();
        }

        private void BackToMainMenu() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
