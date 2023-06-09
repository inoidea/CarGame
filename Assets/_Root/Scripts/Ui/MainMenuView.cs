using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _productId;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonAdsReward;
        [SerializeField] private Button _buttonBuyProduct;
        [SerializeField] private Button _buttonDailyReward;
        [SerializeField] private Button _exitGame;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction showAdsReward, 
            UnityAction<string> buyProduct, UnityAction openShed, UnityAction openDailyReward, UnityAction exitGame)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(openSettings);
            _buttonAdsReward.onClick.AddListener(showAdsReward);
            _buttonBuyProduct.onClick.AddListener(() => buyProduct(_productId));
            _buttonShed.onClick.AddListener(openShed);
            _buttonDailyReward.onClick.AddListener(openDailyReward);
            _exitGame.onClick.AddListener(exitGame);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonAdsReward.onClick.RemoveAllListeners();
            _buttonBuyProduct.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonDailyReward.onClick.RemoveAllListeners();
            _exitGame.onClick.RemoveAllListeners();
        }
    }
}
