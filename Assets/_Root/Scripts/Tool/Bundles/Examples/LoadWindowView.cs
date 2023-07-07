using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _uploadBackgroundButton;

        [Header("Addressables Spawning")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [Header("Addressables Background")]
        [SerializeField] private AssetReference _background;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs = new List<AsyncOperationHandle<GameObject>>();
        private AsyncOperationHandle<Sprite>? _loadedBackground;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _uploadBackgroundButton.onClick.AddListener(UploadBackground);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _uploadBackgroundButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
            RemoveBackground();
        }

        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetImageAssetBundles());
            StartCoroutine(DownloadAndSetAudioAssetBundles());
        }

        private void UploadBackground()
        {
            _uploadBackgroundButton.interactable = false;
            StartCoroutine(UploadBackgroundButton());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab = Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);
            _addressablePrefabs.Add(addressablePrefab);
        }
        
        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private void AddBackground()
        {
            if (!_loadedBackground.HasValue)
            {
                _loadedBackground = Addressables.LoadAssetAsync<Sprite>(_background);
                _loadedBackground.Value.Completed += OnBackgroundLoaded;
            }
        }

        private void RemoveBackground()
        {
            if (_loadedBackground.HasValue && _loadedBackground.Value.IsValid())
            {
                _loadedBackground.Value.Completed -= OnBackgroundLoaded;
                Addressables.Release(_loadedBackground);
                _loadedBackground = null;

                SetBackground(null);
            }
        }

        private void OnBackgroundLoaded(AsyncOperationHandle<Sprite> asyncOperationHandle)
        {
            asyncOperationHandle.Completed -= OnBackgroundLoaded;
            SetBackground(asyncOperationHandle.Result);
        }

        private void SetBackground(Sprite background) => _backgroundImage.sprite = background;
    }
}
