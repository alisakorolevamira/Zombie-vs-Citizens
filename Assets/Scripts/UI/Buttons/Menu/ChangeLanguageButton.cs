using Architecture.Services;
using Architecture.ServicesInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Menu
{
    public class ChangeLanguageButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _languageCode;

        private ILocalizationService _localizationService;

        private void OnEnable()
        {
            _button.onClick.AddListener(SetLanguage);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SetLanguage);
        }

        private void Start()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
        }

        private void SetLanguage() => _localizationService.Localization.ChangeLanguage(_languageCode);
    }
}