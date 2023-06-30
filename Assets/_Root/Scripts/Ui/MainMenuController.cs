using Tool;
using Profile;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSetting, ShowAdsReward, BuyProduct, OpenShed, OpenDailyReward, ExitGame);

            SubscribeAds();
            SubscribeIAP();
        }

        protected override void OnDispose()
        {
            UnSubscribeAds();
            UnSubscribeIAP();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() => _profilePlayer.CurrentState.Value = GameState.Game;

        private void OpenSetting() => _profilePlayer.CurrentState.Value = GameState.Settings;

        private void ShowAdsReward() => ServiceRoster.AdsService.RewardedPlayer.Play();

        private void BuyProduct(string productId) => ServiceRoster.IAPService.Buy(productId);

        private void OpenShed() => _profilePlayer.CurrentState.Value = GameState.Shed;

        private void OpenDailyReward() => _profilePlayer.CurrentState.Value = GameState.DailyReward;

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void SubscribeAds()
        {
            ServiceRoster.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServiceRoster.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            ServiceRoster.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnSubscribeAds()
        {
            ServiceRoster.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServiceRoster.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            ServiceRoster.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void SubscribeIAP()
        {
            ServiceRoster.IAPService.PurchaseSucceed.AddListener(OnIAPSucceed);
            ServiceRoster.IAPService.PurchaseFailed.AddListener(OnIAPFailed);
        }

        private void UnSubscribeIAP()
        {
            ServiceRoster.IAPService.PurchaseSucceed.RemoveListener(OnIAPSucceed);
            ServiceRoster.IAPService.PurchaseFailed.RemoveListener(OnIAPFailed);
        }

        private void OnAdsFinished() => Debug.Log("Реклама просмотрена.");
        private void OnAdsCancelled() => Debug.Log("Реклама не просмотрена.");

        private void OnIAPSucceed() => Debug.Log("Покупка совершена успешно.");
        private void OnIAPFailed() => Debug.Log("Покупка не совершена.");
    }
}
