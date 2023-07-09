using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using Tool.Localization.Examples;
using System.Linq;

namespace Tool.Localization
{
    internal class MainMenuLocalization : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _changeText;

        [Header("Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _localizationTag;

        protected void Start()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            UpdateTextAsync();
        }

        protected void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale _) => UpdateTextAsync();

        private void UpdateTextAsync() =>
            LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;

                        for (int i = 0; i < _changeText.Length; i++)
                        {
                            _changeText[i].text = table.GetEntry(_localizationTag)?.GetLocalizedString();
                        }
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
    }
}
