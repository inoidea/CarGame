using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private InitialSettings _initialSettings;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_initialSettings.CarSpeed, _initialSettings.CarJumpHeight, _initialSettings.State);
        _mainController = new MainController(_placeForUi, profilePlayer);

        _analytics.SendMainMenuOpenedEvent();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => _iapService.Buy("product_1");
}
